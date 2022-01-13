using BeeFor.Core.DomainObjects;
using BeeFor.Core.Interfaces;
using System;

namespace BeeFor.Domain.Entities
{
    public class Projeto : Entity, IAggregteRoot
    {
        public Projeto(Guid id, string nome, Guid idOrganizacao, string idJira, string keyJira, DateTime dataCriacao, Guid responsavelCriacao, DateTime? dataEdicao, Guid? responsavelEdicao, string responsavelJira)
        {
            Id = id;
            Nome = nome;
            IdOrganizacao = idOrganizacao;
            IdJira = idJira;
            KeyJira = keyJira;
            DataCriacao = dataCriacao;
            ResponsavelCriacao = responsavelCriacao;
            DataEdicao = dataEdicao;
            ResponsavelEdicao = responsavelEdicao;
            ResponsavelJira = responsavelJira;
        }

        protected Projeto() { }   
        public string IdJira { get; private set; }
        public string KeyJira { get; private set; }
        public string Descricao { get; private set; }       
        public string ResponsavelJira { get; private set; }
        public Guid ResponsavelCriacao { get; private set; }

        //Ad HoC Set
        public void SetIdProjeto(Guid id)
        {
            Id = id;
        }

        public void SetDescricao()
        {
            Descricao = "Integração Jira";
        }
    }
}
