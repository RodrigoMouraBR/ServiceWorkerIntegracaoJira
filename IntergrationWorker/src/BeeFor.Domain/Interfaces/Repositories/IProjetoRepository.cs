using BeeFor.Domain.Entities;
using BeeFor.Domain.Entities.MongoDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeeFor.Domain.Interfaces.Repositories
{
    public interface IProjetoRepository : IDisposable
    {
        Task<Projeto> ObterProjetoPorIdJira(string idJira);
        Task<Quadro> ObterProjetoQuadroPorIdJira(int idJira);
        Task<List<QuadroColuna>> ObterProjetoQuadroColunaPorIdQuadro(Guid idQuadro);
        Task<QuadroColuna> ObterProjetoQuadroColunaPorIdQuadroColunaJira(int idQuadroColunaJira);
        Task<List<QuadroColunaCard>> ObterProjetoQuadroColunaCardPorIdQuadro(Guid idQuadroColuna);
        Task<QuadroColunaCard> PegarCardPorIdJira(string idColunaCardJira);
        Task<QuadroColuna> PegarColunaPorId(Guid id);
        Task<bool> UpdateProjeto(Projeto projeto);
        Task<bool> UpdateQuadro(Quadro quadro);
        Task<bool> UpdateQuadroColuna(QuadroColuna quadroColuna);
        Task<bool> AddQuadroColuna(QuadroColuna quadroColuna);
        Task<bool> UpdateQuadroColunaCard(QuadroColunaCard quadroColunaCard);
        Task<bool> AddQuadroColunaCard(QuadroColunaCard quadroColunaCard);
        Task<QuadroColunaCard> PegarQuadroColunaCardPorId(Guid id);
        Task AddCardLog(CardLog cardLog);
        Task<bool> ExisteLog(CardLog cardLog);
        Task<QuadroColuna> PegarColunaPorIdJira(int idQuadroColunaJira);
    }
}
