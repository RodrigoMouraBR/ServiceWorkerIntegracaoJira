using BeeFor.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace BeeFor.Domain.Interfaces.Services
{
    public interface IProjetoService : IDisposable
    {
        Task<bool> UpdateProjeto(Projeto projeto);
        Task<bool> UpdateQuadro(Quadro quadro);
        Task<bool> UpdateQuadroColuna(QuadroColuna quadroColuna);
        Task<bool> UpdateQuadroColunaCard(QuadroColunaCard quadroColunaCard);
    }
}
