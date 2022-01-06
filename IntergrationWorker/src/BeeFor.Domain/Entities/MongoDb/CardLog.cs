using System;

namespace BeeFor.Domain.Entities.MongoDb
{
    public class CardLog
    {

        public CardLog(Guid idColunaCard,
                       QuadroColunaCard colunaCard,
                       Guid? idQuadroColunaDe,
                       QuadroColuna quadroColunaDe,
                       Guid? idQuadroColunaPara,
                       QuadroColuna quadroColunaPara,
                       Guid idOrganizacao,
                       DateTime dataCriacao)
        {
            IdColunaCard = idColunaCard;
            ColunaCard = colunaCard;
            IdQuadroColunaDe = idQuadroColunaDe;
            QuadroColunaDe = quadroColunaDe;
            IdQuadroColunaPara = idQuadroColunaPara;
            QuadroColunaPara = quadroColunaPara;
            IdOrganizacao = idOrganizacao;
            DataCriacao = dataCriacao;
            DataMovimentacao = dataCriacao;
            IdPessoaMoveu = Guid.Empty;
        }
        public Guid Id { get; set; }
        public Guid IdColunaCard { get; set; }
        public virtual QuadroColunaCard ColunaCard { get; set; }
        public Guid IdPessoaMoveu { get; set; }
        public Guid? IdQuadroColunaDe { get; set; }
        public virtual QuadroColuna QuadroColunaDe { get; set; }
        public Guid? IdQuadroColunaPara { get; set; }
        public virtual QuadroColuna QuadroColunaPara { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid ResponsavelCriacao { get; set; }
        public Guid IdOrganizacao { get; set; }
        public Guid? IdTimeSource { get; set; }
        public Guid? IdQuadroSource { get; set; }
        public Guid? IdTimeTarget { get; set; }
        public Guid? IdQuadroTarget { get; set; }
        public double TempoPermanenciaColunaDeEmMinutos { get; set; }
    }
}
