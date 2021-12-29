﻿using BeeFor.Core.Interfaces;
using BeeFor.Core.Queue;
using BeeFor.Core.Services;
using BeeFor.Domain.Entities;
using BeeFor.Domain.Interfaces.Repositories;
using BeeFor.Domain.Interfaces.Services;
using BeeFor.Domain.Services.Common;
using BeeFor.Domain.Services.Constants;
using BeeFor.Domain.ValuesObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeeFor.Domain.Services
{
    public class IntegraProjetoJiraService : BaseService, IIntegraProjetoJiraService
    {
        private readonly IProjetoRepository _projetoRepository;

        public IntegraProjetoJiraService(IProjetoRepository projetoRepository, INotifier notifier) : base(notifier)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task ObterProjetosJiraBaseAuthentication(ConfiguracaoIntegracao configuracaoIntegracao)
        {
            var endPoint = URLConstants.obterProjetosJira;
            var response = new ProjetoJiraResponse();
            var projetoJiraResponseList = new List<ProjetoJiraResponse>();
            var projetoJiraResponseInvokeEndPoint = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, response, null, true);

            foreach (var item in projetoJiraResponseInvokeEndPoint.Item2)
            {
                projetoJiraResponseList.Add(item);
            }

            var projetos = await this.ValidarProjetoIntegrado(projetoJiraResponseList);

            foreach (var projeto in projetos)
            {
                var quadro = await this.ValidarProjetoQuadroIntegrado(projeto, configuracaoIntegracao);
                var quadroColuna = await this.ValidarProjetoQuadroColunaIntegrado(quadro, configuracaoIntegracao);
                var quadroColunaCard = await this.ValidarProjetoQuadroColunaCardIntegrado(quadroColuna, configuracaoIntegracao, quadro);
            }
        }
        private async Task<List<Projeto>> ValidarProjetoIntegrado(List<ProjetoJiraResponse> projetoJiraResponseList)
        {
            var projetos = new List<Projeto>();

            foreach (var projetoJiraResponse in projetoJiraResponseList)
            {
                var projetoBeeFor = await _projetoRepository.ObterProjetoPorIdJira(projetoJiraResponse.Id);

                if (projetoBeeFor != null)
                {
                    var _nameProjeto = projetoBeeFor.Nome.Trim() == projetoJiraResponse.Name.Trim();
                    var _keyProjeto = projetoBeeFor.KeyJira.Trim() == projetoJiraResponse.Key.Trim();

                    if (!_nameProjeto || !_keyProjeto)
                    {
                        var projeto = new Projeto(projetoBeeFor.Id, projetoJiraResponse.Name, projetoBeeFor.IdOrganizacao, projetoJiraResponse.Id, projetoJiraResponse.Key, projetoBeeFor.DataCriacao, projetoBeeFor.ResponsavelCriacao, projetoBeeFor.DataEdicao, projetoBeeFor.ResponsavelEdicao, projetoBeeFor.ResponsavelJira);

                        projeto.SetDescricao();

                        projetos.Add(projeto);

                        #region Enviar objeto para o RabbitMQ para fila ProjetoQueue

                        Queue.EnviacardParaFila(projeto, "ProjetoQueue");

                        #endregion
                    }

                    projetos.Add(projetoBeeFor);
                }
            }

            return projetos;
        }
        private async Task<Quadro> ValidarProjetoQuadroIntegrado(Projeto projeto, ConfiguracaoIntegracao configuracaoIntegracao)
        {
            var endPoint = URLConstants.obterQuadroProjetosJira;

            var results = new QuadroProjetoJiraResponse();

            var response = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, null, false);

            var board = response.Item1?.Values?.Where(c => c.Location?.ProjectId == Convert.ToInt32(projeto.IdJira)).FirstOrDefault();

            var QuaadroBeeFor = await _projetoRepository.ObterProjetoQuadroPorIdJira(board.Id);

            var comparar = board.Name.Trim() == QuaadroBeeFor.Nome.Trim();

            if (!comparar)
            {
                var quadro = new Quadro(QuaadroBeeFor.Id,
                                  QuaadroBeeFor.IdTime,
                                  board.Name.Trim(),
                                  QuaadroBeeFor.DataCriacao,
                                  QuaadroBeeFor.ResponsavelCriacao,
                                  QuaadroBeeFor.IdOrganizacao,
                                  QuaadroBeeFor.Ativo,
                                  QuaadroBeeFor.PluginAddProfissionalGoobbePlay,
                                  QuaadroBeeFor.Oculto,
                                  board.Id);

                #region Enviar objeto para o RabbitMQ para fila QuadroQueue

                Queue.EnviacardParaFila(quadro, "QuadroQueue");

                #endregion

                return quadro;
            }

            return QuaadroBeeFor;
        }
        private async Task<List<QuadroColuna>> ValidarProjetoQuadroColunaIntegrado(Quadro quadro, ConfiguracaoIntegracao configuracaoIntegracao)
        {
            var endPoint = URLConstants.obterConfiguracaoQuadroJira;
            var results = new QuadroColunaProjetoJiraResponse();
            var quadroColunaJira = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, quadro.IdQuadroJira, false);
            var quadroColunaBeeFor = await _projetoRepository.ObterProjetoQuadroColunaPorIdQuadro(quadro.Id);

            foreach (var result in quadroColunaJira.Item1.ColumnConfig.Columns)
            {
                var quadroColunaResult = quadroColunaBeeFor.Where(c => c.IdQuadroColunaJira == Convert.ToInt32(result.Statuses[0].Id)).FirstOrDefault();

                //Se a Coluna Existe
                if (quadroColunaResult != null)
                {
                    var compareNome = quadroColunaResult.Nome.Trim() == result.Name.Trim();

                    if (!compareNome)
                    {
                        var quadroColuna = new QuadroColuna(quadroColunaResult.IdQuadro,
                                                            result.Name.Trim(),
                                                            quadroColunaResult.Indice,
                                                            quadroColunaResult.DataCriacao,
                                                            quadroColunaResult.ResponsavelCriacao,
                                                            quadroColunaResult.DataEdicao,
                                                            quadroColunaResult.ResponsavelEdicao,
                                                            quadroColunaResult.IdOrganizacao,
                                                            quadroColunaResult.WipLimit,
                                                            quadroColunaResult.Ativo,
                                                            quadroColunaResult.IdQuadroColunaJira);

                        quadroColuna.SetIdQuadroColuna(quadroColunaResult.Id);

                        Queue.EnviacardParaFila(quadroColuna, "QuadroColunaQueue");
                    }
                }

                //Se a Coluna não Existe
                int indice = 1;
                if (quadroColunaResult == null)
                {
                    var quadroColuna = new QuadroColuna(quadro.Id,
                                                        result.Name.Trim(),
                                                        indice,
                                                        DateTime.Now,
                                                        quadro.ResponsavelCriacao,
                                                        null,
                                                        null,
                                                        quadro.IdOrganizacao,
                                                        0,
                                                        true,
                                                        Convert.ToInt32(result.Statuses[0].Id));

                    quadroColuna.SetIdQuadroColuna(Guid.NewGuid());

                    #region Enviar objeto para o RabbitMQ para fila QuadroColunaQueue

                    Queue.EnviacardParaFila(quadroColuna, "QuadroColunaQueue");

                    indice++;

                    #endregion
                }
            }

            return quadroColunaBeeFor;
        }
        private async Task<List<QuadroColunaCard>> ValidarProjetoQuadroColunaCardIntegrado(List<QuadroColuna> quadroColunas, ConfiguracaoIntegracao configuracaoIntegracao, Quadro quadro)
        {
            var endPoint = URLConstants.obterProjetoQuadroTarefaJira;

            var results = new BoardEpicNoneIssueResponse();

            var projetoQuadroTarefa = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, quadro.IdQuadroJira, false);

            var listIdQuadroCard = new List<string>();

            foreach (var quadroColunaJira in projetoQuadroTarefa.Item1?.Issues)
            {
                var quadroColunaBeeFor = await _projetoRepository.ObterProjetoQuadroColunaPorIdQuadroColunaJira(Convert.ToInt32(quadroColunaJira.Fields.Status.Id));
                var quadroJiraList = projetoQuadroTarefa.Item1?.Issues.Where(c => c.Fields.Status.Id == quadroColunaBeeFor.IdQuadroColunaJira.ToString());

                foreach (var quadroJira in quadroJiraList)
                {
                    var cardConferido = listIdQuadroCard.Where(c => c.Contains(quadroJira.Id)).Any();

                    if (!cardConferido)
                    {
                        var idCardJiraBeeFor = await _projetoRepository.PegarCardPorIdJira(quadroJira.Id);
                        var idColunaJiraBeeFor = idCardJiraBeeFor == null ? null : await _projetoRepository.PegarColunaPorId(idCardJiraBeeFor.IdQuadroColuna);

                        var idCardJira = quadroJira.Id;
                        var idColunaJira = quadroJira.Fields.Status.Id;                      

                        #region NÃO EXISTE O CARD -> ENVIAR PARA FILA PARA SINCRONIZACAO

                        if (idCardJiraBeeFor == null) ParseValidacao_CardNaoExiste(quadroColunaBeeFor, quadroJira);

                        #endregion

                        #region EXISTE O CARD / ESTÁ EM COLUNAS DIFERENTES (JIRA X BEEFOR) PREVALECE O JIRA -> ENVIAR PARA FILA PARA SINCRONIZACAO

                        if (idCardJiraBeeFor != null) ParseValidacao_CardEmColunaDiferente(quadroColunaBeeFor, quadroJira, idColunaJira, idColunaJiraBeeFor, idCardJiraBeeFor);

                        #endregion

                        listIdQuadroCard.Add(quadroJira.Id);
                    }
                }
            }
            return null;
        }
        private void ParseValidacao_CardNaoExiste(QuadroColuna quadroColunaBeeFor, Issue quadroJira)
        {
            var quadroColunaCard = new QuadroColunaCard(quadroColunaBeeFor.Id,
                                                        quadroJira.Fields.Summary,
                                                        quadroJira.Fields.Description,
                                                        1,
                                                        quadroColunaBeeFor.DataCriacao,
                                                        quadroColunaBeeFor.ResponsavelCriacao,
                                                        null,
                                                        null,
                                                        quadroColunaBeeFor.IdOrganizacao,
                                                        false,
                                                        false,
                                                        false,
                                                        quadroJira.Id);
            quadroColunaCard.SetId(Guid.NewGuid());

            Queue.EnviacardParaFila(quadroColunaCard, "QuadroColunaCardQueue");
        }
        private void ParseValidacao_CardEmColunaDiferente(QuadroColuna quadroColunaBeeFor, Issue quadroJira, string idColunaJira, QuadroColuna idColunaJiraBeeFor, QuadroColunaCard idCardJiraBeeFor)
        {
            if (idColunaJiraBeeFor.IdQuadroColunaJira.ToString().Trim() != idColunaJira.Trim())
            {
                var quadroColunaCard = new QuadroColunaCard(quadroColunaBeeFor.Id,
                                                            quadroJira.Fields.Summary,
                                                            quadroJira.Fields.Description,
                                                            1,
                                                            quadroColunaBeeFor.DataCriacao,
                                                            quadroColunaBeeFor.ResponsavelCriacao,
                                                            DateTime.Now,
                                                            null,
                                                            quadroColunaBeeFor.IdOrganizacao,
                                                            false,
                                                            false,
                                                            false,
                                                            quadroJira.Id);
                quadroColunaCard.SetId(idCardJiraBeeFor.Id);

                Queue.EnviacardParaFila(quadroColunaCard, "QuadroColunaCardQueue");
            }
        }
    }
}