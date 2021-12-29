using BeeFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeeFor.Data.Mappings
{
    public class QuadroColunaCardMapping : IEntityTypeConfiguration<QuadroColunaCard>
    {
        public void Configure(EntityTypeBuilder<QuadroColunaCard> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.IdQuadroColuna);
            builder.Property(p => p.Nome).HasColumnType("varchar(2000)");
            builder.Property(p => p.Descricao);
            builder.Property(p => p.Indice).HasColumnType("int");
            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.ResponsavelCriacao);
            builder.Property(p => p.DataEdicao);
            builder.Property(p => p.ResponsavelEdicao);
            builder.Property(p => p.IdOrganizacao);
            builder.Property(p => p.Bloqueado);
            builder.Property(p => p.Arquivado);
            builder.Property(p => p.Backlog);
            builder.Property(p => p.IdColunaCardJira);
           
            builder.ToTable("ColunaCards");
        }
    }
}
