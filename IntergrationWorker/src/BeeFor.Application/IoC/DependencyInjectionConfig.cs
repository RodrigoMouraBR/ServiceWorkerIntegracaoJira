using BeeFor.Application.AutoMapper;
using BeeFor.Application.Interfaces;
using BeeFor.Application.Services;
using BeeFor.Core.Interfaces;
using BeeFor.Core.Notifications;
using BeeFor.Data.Context;
using BeeFor.Data.IoC;
using BeeFor.Data.Repositories;
using BeeFor.Domain.Interfaces.Repositories;
using BeeFor.Domain.Interfaces.Services;
using BeeFor.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            var optionsBuilder = new DbContextOptionsBuilder<BeeForContext>();
            services.AddScoped<BeeForContext>(s => new BeeForContext(optionsBuilder.Options));
            services.AddAutoMapper(typeof(DomainToModelMapping));

            services.ResolveDependenciesMongoDb();

            return services;
        }
    }
}
