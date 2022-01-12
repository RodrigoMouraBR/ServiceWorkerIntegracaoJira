using BeeFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeeFor.Data.Mappings
{
    public class QuadroColunaMapping : IEntityTypeConfiguration<QuadroColuna>
    {
        public void Configure(EntityTypeBuilder<QuadroColuna> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Nome).HasColumnType("varchar(50)");
            builder.Property(p => p.Indice).HasColumnType("int");
            builder.Property(p => p.IdOrganizacao);
            builder.Property(p => p.IdQuadroColunaJira).HasColumnType("int");
            builder.Property(p => p.TipoColuna);
            builder.Property(p => p.DataDesativacao);
            builder.ToTable("QuadroColunas");
        }
    }
}
