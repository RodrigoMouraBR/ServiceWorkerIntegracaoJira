using BeeFor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BeeFor.Data.Context
{
    public class BeeForContext : DbContext
    {
        public BeeForContext( DbContextOptions<BeeForContext> options) : base(options)
        {
        }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Quadro> Quadro { get; set; }
        public DbSet<QuadroColuna> QuadroColuna { get; set; }
        public DbSet<QuadroColunaCard> QuadroColunaCard { get; set; }
        public DbSet<ConfiguracaoIntegracao> ConfiguracaoIntegracoes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BeeForContext).Assembly);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings").GetSection("goobeeteamsDB").Value);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added) entry.Property("DataCriacao").CurrentValue = DateTime.Now;               

                if (entry.State == EntityState.Modified) entry.Property("DataCriacao").IsModified = false;                
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataEdicao") != null))
            {
                if (entry.State == EntityState.Added) entry.Property("DataEdicao").IsModified = false;
               
                if (entry.State == EntityState.Modified) entry.Property("DataEdicao").CurrentValue = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
