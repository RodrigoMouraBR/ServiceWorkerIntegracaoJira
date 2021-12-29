using AutoMapper;
using BeeFor.Application.Interfaces;
using BeeFor.Application.Models;
using BeeFor.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeeFor.Application.Services
{
    public class ConfiguracaoIntegracaoAppService : IConfiguracaoIntegracaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguracaoIntegracaoRepository _configuracaoIntegracaoRepository;

        public ConfiguracaoIntegracaoAppService(IConfiguracaoIntegracaoRepository configuracaoIntegracaoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _configuracaoIntegracaoRepository = configuracaoIntegracaoRepository;
        }

        public async Task<List<ConfiguracaoIntegracaoViewModel>> ConfiguracaoIntegracaoLista()
        {
            var configuracaoIntegracao = _mapper.Map<List<ConfiguracaoIntegracaoViewModel>>(await _configuracaoIntegracaoRepository.ConfiguracaoIntegracaoLista());

            if (configuracaoIntegracao.Count == 0) return null;

            return configuracaoIntegracao;
        }
    }
}
