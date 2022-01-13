using BeeFor.Core.DomainObjects;
using System;

namespace BeeFor.Domain.Entities
{
    public class QuadroColuna : Entity
    {
        public QuadroColuna( 
                            Guid idQuadro, 
                            string nome, 
                            int indice, 
                            DateTime dataCriacao, 
                            Guid responsavelCriacao,
                            DateTime? dataEdicao, 
                            Guid? responsavelEdicao, 
                            Guid idOrganizacao, 
                            int wipLimit, 
                            bool ativo, 
                            int idQuadroColunaJira)
        {
            IdQuadro = idQuadro;
            Nome = nome;
            Indice = indice;
            DataCriacao = dataCriacao;
            ResponsavelCriacao = responsavelCriacao;
            DataEdicao = dataEdicao;
            ResponsavelEdicao = responsavelEdicao;
            IdOrganizacao = idOrganizacao;
            WipLimit = wipLimit;
            Ativo = ativo; 
            IdQuadroColunaJira = idQuadroColunaJira;
        }

        protected QuadroColuna(){}      
        public Guid IdQuadro { get; private set; }        
        public int Indice { get; private set; }  
        public int WipLimit { get; private set; }
        public bool Ativo { get; private set; }
        public int IdQuadroColunaJira { get; private set; }
        public int? TipoColuna { get; private set; }
        public DateTime? DataDesativacao { get; private set; }
        public Guid ResponsavelCriacao { get; private set; }
        public void SetIdQuadroColuna(Guid id)
        {
            Id = id;
        }
        public void SetTipoColuna(int? tipoColuna)
        {
            TipoColuna = tipoColuna;
        }
    }
}
