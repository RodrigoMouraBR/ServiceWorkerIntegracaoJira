using System;
using System.ComponentModel.DataAnnotations;

namespace BeeFor.Application.Models
{
    public class ProjetoViewModel
    {
        [Key]
        public Guid Id { get;  set; }
        public string Nome { get;  set; }
        public Guid IdOrganizacao { get;  set; }
        public string IdJira { get;  set; }
        public string KeyJira { get;  set; }
        public string Descricao { get;  set; }
        public DateTime DataCriacao { get;  set; }
        public Guid ResponsavelCriacao { get;  set; }
        public DateTime? DataEdicao { get;  set; }
        public Guid? ResponsavelEdicao { get;  set; }
        public string ResponsavelJira { get;  set; }
    }
}
