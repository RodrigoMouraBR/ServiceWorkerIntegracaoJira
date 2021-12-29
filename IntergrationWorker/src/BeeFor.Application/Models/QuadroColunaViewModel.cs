using System;
using System.ComponentModel.DataAnnotations;

namespace BeeFor.Application.Models
{
    public class QuadroColunaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdQuadro { get; set; }
        public string Nome { get; set; }
        public int Indice { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid ResponsavelCriacao { get; set; }
        public DateTime? DataEdicao { get; set; }
        public Guid? ResponsavelEdicao { get; set; }
        public Guid IdOrganizacao { get; set; }
        public int WipLimit { get; set; }
        public bool Ativo { get; set; }
        public int IdQuadroColunaJira { get; set; }
    }
}
