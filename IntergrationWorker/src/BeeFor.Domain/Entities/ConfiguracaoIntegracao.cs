using BeeFor.Core.DomainObjects;
using System;

namespace BeeFor.Domain.Entities
{
    public class ConfiguracaoIntegracao : Entity
    {
        public ConfiguracaoIntegracao(Guid idTime, Guid idOrganizacao, string usuario, string chave, string baseUrlJira, DateTime dataCriacao, string responsavelCriacao, DateTime? dataEdicao, Guid? responsavelEdicao)
        {
            IdTime = idTime;
            IdOrganizacao = idOrganizacao;
            Usuario = usuario;
            Chave = chave;
            BaseUrlJira = baseUrlJira;
            DataCriacao = dataCriacao;
            ResponsavelCriacao = responsavelCriacao;
            DataEdicao = dataEdicao;
            ResponsavelEdicao = responsavelEdicao;
        }

        protected ConfiguracaoIntegracao() { }      
        public Guid IdTime { get; private set; }
        public string Usuario { get; private set; }
        public string Chave { get; private set; }
        public string BaseUrlJira { get; private set; }       
        public string ResponsavelCriacao { get; private set; }     
    }
}
