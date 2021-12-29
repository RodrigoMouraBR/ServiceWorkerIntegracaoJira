using BeeFor.Application.IoC;
using BeeFor.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WorkerService1.Interface;

namespace WorkerService1
{
    public class Program
    {
        private static bool _executadoComoServico;
        public static async Task Main(string[] args)
        {
            _executadoComoServico = ExecutadoComoServico(args);

            try
            {
                if (_executadoComoServico)
                {
                    var host = CreateHostBuilder(args).Build();
                    await host.RunAsync();
                }
                else
                {
                    await CreateHostBuilder(args).RunConsoleAsync();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(( services) =>
                {

                    services.AddHostedService<ScopedBackgroundService>();
                    services.AddScoped<IScopedProcessingService, DefaultScopedProcessingService>();   

                    services.ResolveDependencies();                    
                });

            return host;
        }

        
        private static bool ExecutadoComoServico(IEnumerable<string> args) => !(Debugger.IsAttached || args.Contains("--console"));
    }
}
