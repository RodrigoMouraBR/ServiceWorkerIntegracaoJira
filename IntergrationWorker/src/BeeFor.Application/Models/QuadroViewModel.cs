using System;
using System.ComponentModel.DataAnnotations;

namespace BeeFor.Application.Models
{
    public class QuadroViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdTime { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid ResponsavelCriacao { get; set; }
        public Guid IdOrganizacao { get; set; }
        public bool Ativo { get; set; }
        public bool PluginAddProfissionalGoobbePlay { get; set; }
        public bool Oculto { get; private set; }
        public int IdQuadroJira { get; set; }
    }
}
