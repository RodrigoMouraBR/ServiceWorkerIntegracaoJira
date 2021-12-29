using BeeFor.Domain.Entities;
using BeeFor.Domain.Interfaces.Repositories;
using BeeFor.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace BeeFor.Domain.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoService(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public void Dispose()
        {
            _projetoRepository.Dispose();
        }

        public async Task<bool> UpdateProjeto(Projeto projeto)
        {
            projeto.SetDescricao();
            return await _projetoRepository.UpdateProjeto(projeto);
        }

        public async Task<bool> UpdateQuadro(Quadro quadro)
        {
            return await _projetoRepository.UpdateQuadro(quadro);
        }

        public async Task<bool> UpdateQuadroColuna(QuadroColuna quadroColuna)
        {
            var colunaExiste = await _projetoRepository.PegarColunaPorId(quadroColuna.Id);

            if (colunaExiste == null)
                return await _projetoRepository.AddQuadroColuna(quadroColuna);


            if (colunaExiste != null)
                return await _projetoRepository.UpdateQuadroColuna(quadroColuna);

            return false;
        }

        public async Task<bool> UpdateQuadroColunaCard(QuadroColunaCard quadroColunaCard)
        {
           var colunaCardExiste = await _projetoRepository.PegarQuadroColunaCardPorId(quadroColunaCard.Id);

            if (colunaCardExiste != null)
               return await _projetoRepository.UpdateQuadroColunaCard(quadroColunaCard);


            if (colunaCardExiste == null)
                return await _projetoRepository.AddQuadroColunaCard(quadroColunaCard);

            return false;
        }
    }
}
