using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BeeFor.Domain.Entities.MongoDb
{
    public class LogAcaoPrincipal
    {
        public LogAcaoPrincipal(Guid? idPessoaResponsavel, 
                                string nomePessoaResponsavel, 
                                string acao, 
                                Guid? idTime, 
                                Guid idEntidadeAlterada, 
                                string nomeEntidadeAlterada,                                
                                Guid idOrganizacao)
        {
           
            IdPessoaResponsavel = idPessoaResponsavel;
            NomePessoaResponsavel = nomePessoaResponsavel;
            Acao = acao;
            IdTime = idTime;
            IdEntidadeAlterada = idEntidadeAlterada;
            NomeEntidadeAlterada = nomeEntidadeAlterada;           
            IdOrganizacao = idOrganizacao;
            Id = Guid.NewGuid();
            DataAcao = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public Guid? IdPessoaResponsavel { get; private set; }
        public string NomePessoaResponsavel { get; private set; }
        public string Acao { get; private set; }
        public Guid? IdTime { get; private set; }
        public Guid IdEntidadeAlterada { get; private set; }
        public string NomeEntidadeAlterada { get; private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataAcao { get; private set; }
        public Guid IdOrganizacao { get; private set; }
    }
}
