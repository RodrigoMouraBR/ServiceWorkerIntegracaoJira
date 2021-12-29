using System;
using System.ComponentModel.DataAnnotations;

namespace BeeFor.Application.Models
{
    public class QuadroColunaCardViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdQuadroColuna { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Indice { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid ResponsavelCriacao { get; set; }
        public DateTime? DataEdicao { get; set; }
        public Guid? ResponsavelEdicao { get; set; }
        public Guid IdOrganizacao { get; set; }
        public bool Bloqueado { get; set; }
        public bool Arquivado { get; set; }
        public bool Backlog { get; set; }
        public string IdColunaCardJira { get; set; }      
    }
}
