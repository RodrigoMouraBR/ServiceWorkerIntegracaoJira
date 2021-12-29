using AutoMapper;
using BeeFor.Application.Interfaces;
using BeeFor.Application.Models;
using BeeFor.Domain.Entities;
using BeeFor.Domain.Interfaces.Repositories;
using BeeFor.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace BeeFor.Application.Services
{
    public class ProjetoAppService : IProjetoAppService
    {

        private readonly IProjetoService _projetoService;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IMapper _mapper;
        public ProjetoAppService(IProjetoService projetoService, IProjetoRepository projetoRepository, IMapper mapper)
        {
            _projetoService = projetoService;
            _projetoRepository = projetoRepository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _projetoRepository.Dispose();
        }

        public async Task<Projeto> ObterProjetoPorIdJira(ProjetoViewModel projetoViewModel)
        {
            return await _projetoRepository.ObterProjetoPorIdJira(projetoViewModel.IdJira);
        }

        public async Task<bool> UpdateProjeto(ProjetoViewModel projetoViewModel)
        {
            return await _projetoService.UpdateProjeto(_mapper.Map<Projeto>(projetoViewModel));           
        }

        public async Task<bool> UpdateQuadro(QuadroViewModel quadroViewModel)
        {
            return await _projetoService.UpdateQuadro(_mapper.Map<Quadro>(quadroViewModel));
        }
        public async Task<bool> UpdateQuadroColuna(QuadroColunaViewModel quadroColunaViewModel)
        {
            return await _projetoService.UpdateQuadroColuna(_mapper.Map<QuadroColuna>(quadroColunaViewModel));
        }
        public async Task<bool> UpdateQuadroColunaCard(QuadroColunaCardViewModel quadroColunaCardViewModel)
        {
            return await _projetoService.UpdateQuadroColunaCard(_mapper.Map<QuadroColunaCard>(quadroColunaCardViewModel));
        }
    }
}
