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

        protected Projeto() {
            
        }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }    
        public Guid IdOrganizacao { get; private set; }
        public string IdJira { get; private set; }
        public string KeyJira { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Guid ResponsavelCriacao { get; private set; }
        public DateTime? DataEdicao { get; private set; }
        public Guid? ResponsavelEdicao { get; private set; }
        public string ResponsavelJira { get; private set; }

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
