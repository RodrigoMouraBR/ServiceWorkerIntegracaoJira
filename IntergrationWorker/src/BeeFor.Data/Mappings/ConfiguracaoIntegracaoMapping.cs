using BeeFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeeFor.Data.Mappings
{
    public class ConfiguracaoIntegracaoMapping : IEntityTypeConfiguration<ConfiguracaoIntegracao>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoIntegracao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdTime);
            builder.Property(p => p.IdOrganizacao);
            builder.Property(p => p.Usuario).HasColumnType("varchar(150)");
            builder.Property(p => p.Chave).HasColumnType("varchar(150)");
            builder.Property(p => p.BaseUrlJira).HasColumnType("varchar(100)");
            builder.Property(p => p.DataCriacao).HasColumnType("datetime");
            builder.Property(p => p.ResponsavelCriacao);
            builder.Property(p => p.DataEdicao).HasColumnType("datetime2");
            builder.Property(p => p.ResponsavelEdicao);

            builder.ToTable("ConfiguracaoIntegracoes");
        }
    }
}
