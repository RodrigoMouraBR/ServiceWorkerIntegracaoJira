using BeeFor.Core.DomainObjects;
using System;
namespace BeeFor.Domain.Entities
{
    public class QuadroColunaCard : Entity
    {
        
        protected QuadroColunaCard() { }

        public QuadroColunaCard(Guid idQuadroColuna, 
                                string nome, 
                                string descricao, 
                                int indice, 
                                DateTime dataCriacao, 
                                Guid responsavelCriacao, 
                                DateTime? dataEdicao, 
                                Guid? responsavelEdicao, 
                                Guid idOrganizacao, 
                                bool bloqueado, 
                                bool arquivado, 
                                bool backlog, 
                                string idColunaCardJira)
        {
            IdQuadroColuna = idQuadroColuna;
            Nome = nome;
            Descricao = descricao;
            Indice = indice;
            DataCriacao = dataCriacao;
            ResponsavelCriacao = responsavelCriacao;
            DataEdicao = dataEdicao;
            ResponsavelEdicao = responsavelEdicao;
            IdOrganizacao = idOrganizacao;
            Bloqueado = bloqueado;
            Arquivado = arquivado;
            Backlog = backlog;
            IdColunaCardJira = idColunaCardJira;
        }

        public Guid Id { get; private set; }
        public Guid IdQuadroColuna { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int Indice { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Guid ResponsavelCriacao { get; private set; }
        public DateTime? DataEdicao { get; private set; }
        public Guid? ResponsavelEdicao { get; private set; }
        public Guid IdOrganizacao { get; private set; }
        public bool Bloqueado { get; private set; }
        public bool Arquivado { get; private set; }
        public bool Backlog { get; private set; }
        public string IdColunaCardJira { get; private set; }
        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetArquivado(bool arquivado)
        {
            Arquivado = arquivado;
        }
    }
}
