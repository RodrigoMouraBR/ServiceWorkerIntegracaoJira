using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeFor.Domain.Entities.MongoDb
{
    public class LogIteracaoCard
    {
        public Guid Id { get; set; }
        public Guid IdIteracao { get; set; }
        //public virtual Iteracao Iteracao { get; set; }
        public virtual ICollection<QuadroColunaCard> ColunaCards { get; set; }       
        public DateTime DataCriacao { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DataEdicao { get; set; }
        public Guid IdOrganizacao { get; set; }
    }
}
