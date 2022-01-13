using System;

namespace BeeFor.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
          
        }
        public Guid Id { get; protected set; }
        public Guid IdOrganizacao { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime DataCriacao { get; protected set; }        
        public DateTime? DataEdicao { get; protected set; }
        public Guid? ResponsavelEdicao { get; protected set; }
    }
}
