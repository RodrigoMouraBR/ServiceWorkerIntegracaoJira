using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BeeFor.Domain.Entities.MongoDb
{
    public class CardEntregueLog
    {
        public Guid Id { get; set; }
        public Guid IdCard { get; set; }
        public virtual QuadroColunaCard Card { get; set; }
        public Guid IdPessoaMoveu { get; set; }
        // public virtual Pessoa PessoaMoveu { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataEntrega { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataCriacao { get; set; }
        public Guid ResponsavelCriacao { get; set; }
        public bool Retrocedeu { get; set; }
        public Guid IdTime { get; set; }
        public Guid IdOrganizacao { get; set; }
        public Guid? IdIteracao { get; set; }
    }
}
