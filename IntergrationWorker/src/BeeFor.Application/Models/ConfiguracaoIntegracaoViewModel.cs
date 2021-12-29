using System;
using System.ComponentModel.DataAnnotations;

namespace BeeFor.Application.Models
{
    public class ConfiguracaoIntegracaoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdTime { get; set; }
        public Guid IdOrganizacao { get; set; }
        public string Usuario { get; set; }
        public string Chave { get; set; }
        public string BaseUrlJira { get; set; }
        public DateTime DataCriacao { get; set; }
        public string ResponsavelCriacao { get; set; }
        public DateTime? DataEdicao { get; set; }
        public Guid? ResponsavelEdicao { get; set; }
    }
}
