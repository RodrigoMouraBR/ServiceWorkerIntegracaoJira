using BeeFor.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerService1.Interface;

namespace WorkerService1
{
    public class DefaultScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger<DefaultScopedProcessingService> _logger;
        private readonly IConfiguracaoIntegracaoAppService _configuracaoIntegracaoAppService;
        private readonly IIntegraProjetoJiraAppService _integraProjetoJiraAppService;

        public DefaultScopedProcessingService(ILogger<DefaultScopedProcessingService> logger, IConfiguracaoIntegracaoAppService configuracaoIntegracaoAppService, IIntegraProjetoJiraAppService integraProjetoJiraAppService)
        {
            _logger = logger;
            _configuracaoIntegracaoAppService = configuracaoIntegracaoAppService;
            _integraProjetoJiraAppService = integraProjetoJiraAppService;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Novo Processo...", DateTime.Now);

                var configuracaoToken = await _configuracaoIntegracaoAppService.ConfiguracaoIntegracaoLista();

                foreach (var config in configuracaoToken)
                {
                    await _integraProjetoJiraAppService.ObterProjetosJiraBaseAuthentication(config);
                }

                _logger.LogInformation("Fim do Processo...", DateTime.Now);
                Console.WriteLine("===>");
                Console.WriteLine("===>");
                await Task.Delay(60_000, stoppingToken);
            }
        }
    }
}
