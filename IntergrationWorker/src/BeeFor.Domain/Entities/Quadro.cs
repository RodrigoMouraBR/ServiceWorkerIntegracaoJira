using BeeFor.Core.DomainObjects;
using System;

namespace BeeFor.Domain.Entities
{
    public class Quadro : Entity
    {
        public Quadro(Guid id, Guid idTime, string nome, DateTime dataCriacao, Guid responsavelCriacao, Guid idOrganizacao, bool ativo, bool pluginAddProfissionalGoobbePlay, bool oculto, int idQuadroJira)
        {
            Id = id;
            IdTime = idTime;
            Nome = nome;
            DataCriacao = dataCriacao;
            ResponsavelCriacao = responsavelCriacao;
            IdOrganizacao = idOrganizacao;
            Ativo = ativo;
            PluginAddProfissionalGoobbePlay = pluginAddProfissionalGoobbePlay;
            Oculto = oculto;
            IdQuadroJira = idQuadroJira;
        }

        public Guid Id { get; private set; }
        public Guid IdTime { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Guid ResponsavelCriacao { get; private set; }
        public Guid IdOrganizacao { get; private set; }
        public bool Ativo { get; private set; }
        public bool PluginAddProfissionalGoobbePlay { get; private set; }
        public bool Oculto { get; private set; }
        public int IdQuadroJira { get; private set; }

        public void SetPluginAddProfissionalGoobeePlay()
        {
            PluginAddProfissionalGoobbePlay = true;
        }
    }
}
