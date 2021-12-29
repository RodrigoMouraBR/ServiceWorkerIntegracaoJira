using BeeFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeeFor.Data.Mappings
{
    public class ProjetoMapping : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Nome).HasColumnType("varchar(50)");
            builder.Property(p => p.IdOrganizacao);
            builder.Property(p => p.IdJira);
            builder.Property(p => p.KeyJira);
            builder.Property(p => p.Descricao).HasColumnType("varchar(1000)");
            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.ResponsavelCriacao);
            builder.Property(p => p.DataEdicao);
            builder.Property(p => p.ResponsavelEdicao);
            builder.Property(p => p.ResponsavelJira);           
            builder.ToTable("Projetos");
        }
    }
}
