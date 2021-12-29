using BeeFor.Application.AutoMapper;
using BeeFor.Application.Interfaces;
using BeeFor.Application.Services;
using BeeFor.Core.Interfaces;
using BeeFor.Core.Notifications;
using BeeFor.Data.Context;
using BeeFor.Data.Repositories;
using BeeFor.Domain.Interfaces.Repositories;
using BeeFor.Domain.Interfaces.Services;
using BeeFor.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace BeeFor.Application.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IConfiguracaoIntegracaoAppService, ConfiguracaoIntegracaoAppService>()
                    .AddScoped<IIntegraProjetoJiraService, IntegraProjetoJiraService>()
                    .AddScoped<IIntegraProjetoJiraAppService, IntegraProjetoJiraAppService>()
                    .AddScoped<IProjetoAppService, ProjetoAppService>()
                    .AddScoped<IProjetoService, ProjetoService>()
                    .AddScoped<IConfiguracaoIntegracaoRepository, ConfiguracaoIntegracaoRepository>()
                    .AddScoped<IProjetoRepository, ProjetoRepository>()
                    .AddScoped<INotifier, Notifier>();

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BeeForContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("goobeeteamsDB"));

            services.AddScoped<BeeForContext>(s => new BeeForContext(optionsBuilder.Options));

            services.AddAutoMapper(typeof(DomainToModelMapping));

            return services;
        }
    }
}
