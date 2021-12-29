using BeeFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeeFor.Data.Mappings
{
    public class QuadroMapping : IEntityTypeConfiguration<Quadro>
    {
        public void Configure(EntityTypeBuilder<Quadro> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdTime);
            builder.Property(p => p.Nome).HasColumnType("varchar(50)");
            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.ResponsavelCriacao);
            builder.Property(p => p.IdOrganizacao);
            builder.Property(p => p.Ativo);
            builder.Property(p => p.PluginAddProfissionalGoobbePlay);
            builder.Property(p => p.Oculto);
            builder.Property(p => p.IdQuadroJira).HasColumnType("int");
            builder.ToTable("Quadros");
        }
    }
}
