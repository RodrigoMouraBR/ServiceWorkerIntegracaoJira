using BeeFor.Application.Models;
using BeeFor.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace BeeFor.Application.Interfaces
{
    public interface IProjetoAppService : IDisposable
    {
        Task<bool> UpdateProjeto(ProjetoViewModel projetoViewModel);
        Task<bool> UpdateQuadro(QuadroViewModel projetoViewModel);
        Task<bool> UpdateQuadroColuna(QuadroColunaViewModel quadroColunaViewModel);
        Task<bool> UpdateQuadroColunaCard(QuadroColunaCardViewModel quadroColunaCardViewModel);
    }
}
