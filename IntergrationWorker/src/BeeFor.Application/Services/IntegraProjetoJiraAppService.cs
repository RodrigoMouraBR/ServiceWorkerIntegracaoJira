using AutoMapper;
using BeeFor.Application.Interfaces;
using BeeFor.Application.Models;
using BeeFor.Domain.Entities;
using BeeFor.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace BeeFor.Application.Services
{
    public class IntegraProjetoJiraAppService : IIntegraProjetoJiraAppService
    {
        private readonly IMapper _mapper;
        private readonly IIntegraProjetoJiraService _integraProjetoJiraService;

        public IntegraProjetoJiraAppService(IIntegraProjetoJiraService integraProjetoJiraService, IMapper mapper)
        {
            _integraProjetoJiraService = integraProjetoJiraService;
            _mapper = mapper;
        }

        public async Task ObterProjetosJiraBaseAuthentication(ConfiguracaoIntegracaoViewModel listConfiguracaoIntegracaoViewModel)
        {
            await _integraProjetoJiraService.ObterProjetosJiraBaseAuthentication(_mapper.Map<ConfiguracaoIntegracao>(listConfiguracaoIntegracaoViewModel));
        }
    }
}
