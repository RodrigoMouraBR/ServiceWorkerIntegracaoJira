using BeeFor.Core.Interfaces;
using BeeFor.Core.Queue;
using BeeFor.Core.Services;
using BeeFor.Domain.Entities;
using BeeFor.Domain.Entities.MongoDb;
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
                var _ = await this.ValidarProjetoQuadroColunaIntegrado(quadro, configuracaoIntegracao);
                var quadroColunaCardsResults = await this.ValidarProjetoQuadroColunaCardIntegrado(configuracaoIntegracao, quadro);
                await ParseValidacao_ArquivarCard(quadro, quadroColunaCardsResults);
                await ValidarChangelogIntegrado(configuracaoIntegracao, quadro);
            }
        }

        #region Projeto
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
        #endregion

        #region Quadro
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
        #endregion

        #region Quadro Coluna
        private async Task<List<QuadroColuna>> ValidarProjetoQuadroColunaIntegrado(Quadro quadro, ConfiguracaoIntegracao configuracaoIntegracao)
        {
            var endPoint = URLConstants.obterConfiguracaoQuadroJira;
            var results = new QuadroColunaProjetoJiraResponse();
            var quadroColunaJira = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, quadro.IdQuadroJira, false);
            var quadroColunaBeeFor = await _projetoRepository.ObterProjetoQuadroColunaPorIdQuadro(quadro.Id);

            int indexColumn = 1;
            foreach (var result in quadroColunaJira.Item1.ColumnConfig.Columns)
            {
                var quadroColunaResult = quadroColunaBeeFor.Where(c => c.IdQuadroColunaJira == Convert.ToInt32(result.Statuses[0].Id)).FirstOrDefault();

                if (quadroColunaResult != null) await this.ParseValidacao_ColunaExiste(quadro, quadroColunaResult, result, indexColumn, configuracaoIntegracao);

                if (quadroColunaResult == null) await this.ParseValidacao_ColunaNaoExiste(quadro, result, indexColumn, configuracaoIntegracao);

                indexColumn++;
            }

            return quadroColunaBeeFor;
        }
        private async Task ParseValidacao_ColunaNaoExiste(Quadro quadro, Column result, int indexColumn, ConfiguracaoIntegracao configuracaoIntegracao)
        {
            var colunaNOmeResponse = await Parse_NomeColunaQuadro(result.Statuses[0].Id, configuracaoIntegracao);

            var nomeDefinidoParaColuna = colunaNOmeResponse.UntranslatedName; /*DEFINIÇÃO DO NOME DA COLUNA*/

            #region
            var quadroColuna = new QuadroColuna(quadro.Id, nomeDefinidoParaColuna, indexColumn, DateTime.Now, quadro.ResponsavelCriacao, null, null, quadro.IdOrganizacao, 0, true, Convert.ToInt32(result.Statuses[0].Id));

            quadroColuna.SetIdQuadroColuna(Guid.NewGuid());
            #endregion

            #region Enviar objeto para o RabbitMQ para fila QuadroColunaQueue

            Queue.EnviacardParaFila(quadroColuna, "QuadroColunaQueue");

            #endregion
        }
        private async Task ParseValidacao_ColunaExiste(Quadro quadro, QuadroColuna quadroColunaResult, Column result, int indexColumn, ConfiguracaoIntegracao configuracaoIntegracao)
        {
            bool compareTipoColuna;
            var colunaNOmeResponse = await Parse_NomeColunaQuadro(result.Statuses[0].Id, configuracaoIntegracao);

            var nomeDefinidoParaColuna = colunaNOmeResponse.UntranslatedName; /*DEFINIÇÃO DO NOME DA COLUNA*/

            var compareNome = quadroColunaResult.Nome.Trim() == colunaNOmeResponse.UntranslatedName.Trim();
            var indexCompare = quadroColunaResult.Indice == indexColumn;    
            var entradaSaida = await Parse_EntradaSaida(quadro, configuracaoIntegracao, quadroColunaResult);

            switch (entradaSaida)
            {
                case "new":
                    compareTipoColuna = quadroColunaResult.TipoColuna == 1;
                    break;

                case "indeterminate":
                    compareTipoColuna = quadroColunaResult.TipoColuna == null;
                    break;

                case "done":
                    compareTipoColuna = quadroColunaResult.TipoColuna == 2;
                    break;

                default:
                    compareTipoColuna = true;
                    break;
            }

            if (!compareNome || !indexCompare || !compareTipoColuna)
            {
                var quadroColuna = new QuadroColuna(quadroColunaResult.IdQuadro,
                                                     nomeDefinidoParaColuna.Trim(), 
                                                     indexColumn, 
                                                     quadroColunaResult.DataCriacao, 
                                                     quadroColunaResult.ResponsavelCriacao,
                                                     quadroColunaResult.DataEdicao, 
                                                     quadroColunaResult.ResponsavelEdicao, 
                                                     quadroColunaResult.IdOrganizacao, 
                                                     quadroColunaResult.WipLimit,
                                                     quadroColunaResult.Ativo, 
                                                     quadroColunaResult.IdQuadroColunaJira);

                quadroColuna.SetIdQuadroColuna(quadroColunaResult.Id);
                switch (entradaSaida)
                {
                    case "new":
                        quadroColuna.SetTipoColuna(1);
                        break;

                    case "indeterminate":
                        quadroColuna.SetTipoColuna(null);
                        break;

                    case "done":
                        quadroColuna.SetTipoColuna(2);
                        break;

                    default:
                        quadroColuna.SetTipoColuna(null);
                        break;
                }               

                Queue.EnviacardParaFila(quadroColuna, "QuadroColunaQueue");
            }
        }
        private async Task<ColunaNomeReponse> Parse_NomeColunaQuadro(string id, ConfiguracaoIntegracao configuracaoIntegracao)
        {
            var endPoint = URLConstants.obterConfiguracaoQuadroJiraNomeColuna;
            var results = new ColunaNomeReponse();
            var colunaNomeReponse = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, Convert.ToInt32(id), false);
            return colunaNomeReponse.Item1;
        }
        private async Task<string> Parse_EntradaSaida(Quadro quadro, ConfiguracaoIntegracao configuracaoIntegracao, QuadroColuna quadroColuna)
        {
            var endPoint = URLConstants.obterProjetoQuadroTarefaJira_Changelog;

            var results = new BoardEpicNoneIssueResponse();

            var projeto = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, quadro.IdQuadroJira, false);

            string tipoColuna = "";

            foreach (var item in projeto.Item1?.Issues)
            {
                var quadroCompare = item.Fields?.Status?.Id.Trim() == quadroColuna.IdQuadroColunaJira.ToString().Trim();

                if (quadroCompare)
                {
                    tipoColuna = item.Fields?.Status?.StatusCategory?.Key;
                }

            }

            return tipoColuna;
        }

        #endregion

        #region Quadro Cards
        private async Task<BoardEpicNoneIssueResponse> ValidarProjetoQuadroColunaCardIntegrado(ConfiguracaoIntegracao configuracaoIntegracao, Quadro quadro)
        {
            var endPoint = URLConstants.obterProjetoQuadroTarefaJira_Changelog;

            var results = new BoardEpicNoneIssueResponse();

            var projetoQuadroTarefa = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, quadro.IdQuadroJira, false);

            var listIdQuadroCard = new List<string>();
            int indexCard = 1;
            foreach (var quadroColunaJira in projetoQuadroTarefa.Item1?.Issues)
            {
                var quadroColunaBeeFor = await _projetoRepository.ObterProjetoQuadroColunaPorIdQuadroColunaJira(Convert.ToInt32(quadroColunaJira.Fields.Status.Id));
                var quadroJiraList = projetoQuadroTarefa.Item1?.Issues.Where(c => c.Fields.Status.Id == quadroColunaBeeFor.IdQuadroColunaJira.ToString());
               
                foreach (var quadroJira in quadroJiraList)
                {
                    var cardConferido = listIdQuadroCard.Where(c => c.Contains(quadroJira.Id)).Any();

                   
                    if (!cardConferido)
                    {
                        var _quadroColunaCard = await _projetoRepository.PegarCardPorIdJira(quadroJira.Id);
                        var _quadroColuna = _quadroColunaCard == null ? null : await _projetoRepository.PegarColunaPorId(_quadroColunaCard.IdQuadroColuna);

                        #region NÃO EXISTE O CARD -> ENVIAR PARA FILA PARA SINCRONIZACAO

                        if (_quadroColunaCard == null) ParseValidacao_CardNaoExiste(quadroColunaBeeFor, quadroJira, indexCard);

                        #endregion

                        #region EXISTE O CARD / ESTÁ EM COLUNAS DIFERENTES (JIRA X BEEFOR) PREVALECE O JIRA -> ENVIAR PARA FILA PARA SINCRONIZACAO
                        if (_quadroColunaCard != null)
                        {
                            ParseValidacao_CardEmColunaDiferente(quadroColunaBeeFor, quadroJira, quadroJira.Fields.Status.Id, _quadroColuna, _quadroColunaCard, indexCard);
                        }
                        #endregion

                        listIdQuadroCard.Add(quadroJira.Id);
                        indexCard++;
                    }

                    
                }
            }
            return projetoQuadroTarefa.Item1;
        }
        private void ParseValidacao_CardNaoExiste(QuadroColuna quadroColunaBeeFor, Issue quadroJira, int indexCard)
        {
            var quadroColunaCard = new QuadroColunaCard(quadroColunaBeeFor.Id,
                                                        quadroJira.Fields.Summary,
                                                        quadroJira.Fields.Description,
                                                        indexCard,
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
        private void ParseValidacao_CardEmColunaDiferente(QuadroColuna quadroColunaBeeFor, Issue quadroJira, string idColunaJira, QuadroColuna idColunaJiraBeeFor, QuadroColunaCard _quadroColunaCard, int indexCard)
        {
            if (idColunaJiraBeeFor.IdQuadroColunaJira.ToString().Trim() != idColunaJira.Trim() || _quadroColunaCard.Indice != indexCard)
            {
                var quadroColunaCard = new QuadroColunaCard(quadroColunaBeeFor.Id,
                                                            quadroJira.Fields.Summary,
                                                            quadroJira.Fields.Description,
                                                            indexCard,
                                                            quadroColunaBeeFor.DataCriacao,
                                                            quadroColunaBeeFor.ResponsavelCriacao,
                                                            DateTime.Now,
                                                            null,
                                                            quadroColunaBeeFor.IdOrganizacao,
                                                            false,
                                                            false,
                                                            false,
                                                            quadroJira.Id);
                quadroColunaCard.SetId(_quadroColunaCard.Id);

                Queue.EnviacardParaFila(quadroColunaCard, "QuadroColunaCardQueue");
            }
        }
        private async Task ParseValidacao_ArquivarCard(Quadro quadro, BoardEpicNoneIssueResponse boardEpicNoneIssueResponse)
        {
            //Buscar as colunas pelo id do quadro
            var quadroColunasBeeFor = await _projetoRepository.ObterProjetoQuadroColunaPorIdQuadro(quadro.Id);

            foreach (var quadroColunaBeeFor in quadroColunasBeeFor)
            {
                //Buscar os cards pelo id coluna
                var cardsBeeFor = await _projetoRepository.ObterProjetoQuadroColunaCardPorIdQuadro(quadroColunaBeeFor.Id);

                foreach (var cardBeeFor in cardsBeeFor)
                {
                    var quadroColunaJira = boardEpicNoneIssueResponse.Issues.Where(c => c.Id == cardBeeFor.IdColunaCardJira).Any();

                    if (!quadroColunaJira)
                    {
                        if (!cardBeeFor.Arquivado)
                        {
                            cardBeeFor.SetArquivado(true);
                            await _projetoRepository.UpdateQuadroColunaCard(cardBeeFor);
                        }
                    }
                }
            }
        }
        #endregion

        #region Logs
        private async Task ValidarChangelogIntegrado(ConfiguracaoIntegracao configuracaoIntegracao, Quadro quadro)
        {
            var endPoint = URLConstants.obterProjetoQuadroTarefaJira_Changelog;

            var results = new BoardEpicNoneIssueResponse();

            var projetoQuadroTarefa = await InvokeEndPoint.Invoke(configuracaoIntegracao, endPoint, results, quadro.IdQuadroJira, false);

            foreach (var quadroColunaJira in projetoQuadroTarefa.Item1?.Issues)
            {
                var quadroColunaBeeFor = await _projetoRepository.ObterProjetoQuadroColunaPorIdQuadroColunaJira(Convert.ToInt32(quadroColunaJira.Fields.Status.Id));
                var quadroJiraList = projetoQuadroTarefa.Item1?.Issues
                    .Where(c => c.Fields.Status.Id == quadroColunaBeeFor.IdQuadroColunaJira.ToString());

                var quadroColunaCard = await _projetoRepository.PegarCardPorIdJira(quadroColunaJira.Id);

                if (quadroColunaCard != null)
                {
                    foreach (var histories in quadroColunaJira?.Changelog?.Histories)
                    {
                        foreach (var history in histories.Items.Where(x => x.Field.ToLower() == "status"))
                        {
                            _ = int.TryParse(history.From, out int idQuadroColunaJiraDe);
                            _ = int.TryParse(history.To, out int idQuadroColunaJiraPara);
                            _ = DateTime.TryParse(histories.Created, out DateTime dataCriacao);

                            var quadroColunaCardDe = await _projetoRepository.PegarColunaPorIdJira(idQuadroColunaJiraDe);
                            var quadroColunaCardPara = await _projetoRepository.PegarColunaPorIdJira(idQuadroColunaJiraPara);

                            CardLog cardLog = new CardLog(
                                quadroColunaCard.Id,
                                quadroColunaCard,
                                quadroColunaCardDe.Id,
                                quadroColunaCardDe,
                                quadroColunaCardPara.Id,
                                quadroColunaCardPara,
                                quadroColunaCard.IdOrganizacao,
                                dataCriacao);

                            bool existelog = await _projetoRepository.ExisteLog(cardLog);
                            if (!existelog)
                                await _projetoRepository.AddCardLog(cardLog);
                        }
                    }
                }

            }
        }
        #endregion
    }
}
