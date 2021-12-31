using BeeFor.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BeeFor.Data.IoC
{
    public static class DependencyInjectionMongDbConfig
    {
        public static IServiceCollection ResolveDependenciesMongoDb(this IServiceCollection services)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            MongoDbContext.ConnectionString = configuration.GetSection("ConnectionStrings").GetSection("MongoConnectionString").Value;
            MongoDbContext.DatabaseName = configuration.GetSection("ConnectionStrings").GetSection("MongoDatabase").Value;
            MongoDbContext.IsSSL = Convert.ToBoolean(configuration.GetSection("ConnectionStrings").GetSection("MongoIsSSL").Value);

            return services;
        }
    }
}
