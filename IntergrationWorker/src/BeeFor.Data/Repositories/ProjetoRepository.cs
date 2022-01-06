using BeeFor.Core.Constants;
using BeeFor.Data.Context;
using BeeFor.Domain.Entities;
using BeeFor.Domain.Entities.MongoDb;
using BeeFor.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeeFor.Data.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        protected readonly BeeForContext _context;
        protected readonly MongoDbContext _mongoDbContext;
        public ProjetoRepository(BeeForContext context)
        {
            _context = context;
            _mongoDbContext = new MongoDbContext();
        }

        #region Consultas
        public async Task<Projeto> ObterProjetoPorIdJira(string idJira)
        {
            return await _context.Projeto.Where(x => x.IdJira == idJira.Trim()).AsNoTracking().SingleOrDefaultAsync();
        }
        public async Task<Quadro> ObterProjetoQuadroPorIdJira(int idJira)
        {
            return await _context.Quadro.Where(x => x.IdQuadroJira == idJira).AsNoTracking().SingleOrDefaultAsync();
        }
        public async Task<List<QuadroColuna>> ObterProjetoQuadroColunaPorIdQuadro(Guid idQuadro)
        {
            return await _context.QuadroColuna.Where(x => x.IdQuadro == idQuadro).AsNoTracking().ToListAsync();
        }
        public async Task<QuadroColuna> ObterProjetoQuadroColunaPorIdQuadroColunaJira(int idQuadroColunaJira)
        {
            return await _context.QuadroColuna.Where(x => x.IdQuadroColunaJira == idQuadroColunaJira).AsNoTracking().SingleOrDefaultAsync();
        }
        public async Task<List<QuadroColunaCard>> ObterProjetoQuadroColunaCardPorIdQuadro(Guid idQuadroColuna)
        {
            return await _context.QuadroColunaCard.Where(x => x.IdQuadroColuna == idQuadroColuna).AsNoTracking().ToListAsync();
        }
        public async Task<QuadroColunaCard> PegarCardPorIdJira(string idColunaCardJira)
        {
            return await _context.QuadroColunaCard.Where(x => x.IdColunaCardJira == idColunaCardJira).AsNoTracking().SingleOrDefaultAsync();
        }
        public async Task<QuadroColuna> PegarColunaPorId(Guid id)
        {
            return await _context.QuadroColuna.Where(x => x.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }
        public async Task<QuadroColunaCard> PegarQuadroColunaCardPorId(Guid id)
        {
            return await _context.QuadroColunaCard.Where(x => x.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }
        public async Task<QuadroColuna> PegarColunaPorIdJira(int idQuadroColunaJira)
        {
            return await _context.QuadroColuna.Where(x => x.IdQuadroColunaJira == idQuadroColunaJira).AsNoTracking().SingleOrDefaultAsync();
        }
        #endregion

        #region Update
        public async Task<bool> UpdateProjeto(Projeto projeto)
        {
            var entry = _context.Projeto.FirstOrDefault(e => e.Id == projeto.Id);
            _context.Entry(entry).CurrentValues.SetValues(projeto);
            var result = await _context.SaveChangesAsync();
            if (result == 1) return true;

            return false;
        }
        public async Task<bool> UpdateQuadro(Quadro quadro)
        {
            var entry = _context.Quadro.FirstOrDefault(e => e.Id == quadro.Id);
            _context.Entry(entry).CurrentValues.SetValues(quadro);
            var result = await _context.SaveChangesAsync();
            if (result == 1) return true;

            return false;
        }
        public async Task<bool> UpdateQuadroColuna(QuadroColuna quadroColuna)
        {
            var entry = _context.QuadroColuna.FirstOrDefault(e => e.Id == quadroColuna.Id);
            _context.Entry(entry).CurrentValues.SetValues(quadroColuna);
            var result = await _context.SaveChangesAsync();

            if (result == 1) return true;

            return false;
        }
        public async Task<bool> UpdateQuadroColunaCard(QuadroColunaCard quadroColunaCard)
        {

            var entry = _context.QuadroColunaCard.FirstOrDefault(e => e.Id == quadroColunaCard.Id);
            _context.Entry(entry).CurrentValues.SetValues(quadroColunaCard);
            var result = await _context.SaveChangesAsync();
            if (result == 1) return true;

            return false;
        }
        #endregion

        #region Create
        public async Task<bool> AddQuadroColuna(QuadroColuna quadroColuna)
        {
            await _context.QuadroColuna.AddAsync(quadroColuna);
            var result = await _context.SaveChangesAsync();

            if (result == 1) return true;

            return false;
        }
        public async Task<bool> AddQuadroColunaCard(QuadroColunaCard quadroColunaCard)
        {
            await _context.QuadroColunaCard.AddAsync(quadroColunaCard);
            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                await AddLogAcaoPrincipal(quadroColunaCard.ResponsavelCriacao, string.Empty, ConstantsValues.criouCard, null, quadroColunaCard.Id, quadroColunaCard.Nome, quadroColunaCard.IdOrganizacao);
                return true;
            }

            return false;
        }
        #endregion


        #region Logs MongoDb

        private async Task AddLogAcaoPrincipal(Guid? responsavelCriacao,
                                                string nomePessoaResponsavel,
                                                string acao,
                                                Guid? idTime,
                                                Guid IdEntidadeAlterada,
                                                string nomeEntidadeAlterada,
                                                Guid IdOrganizacao)
        {
            var logAcaoPrincipal = new LogAcaoPrincipal(responsavelCriacao, nomePessoaResponsavel, acao, idTime, IdEntidadeAlterada, nomeEntidadeAlterada, IdOrganizacao);
            await _mongoDbContext.LogsAcoesPrincipais.InsertOneAsync(logAcaoPrincipal);
        }

        public async Task<bool> ExisteLog(CardLog cardLog)
        {
            FilterDefinition<CardLog> filter = Builders<CardLog>.Filter
                .Where(x => x.IdColunaCard == cardLog.IdColunaCard
                && x.IdQuadroColunaDe == cardLog.IdQuadroColunaDe
                && x.IdQuadroColunaPara == cardLog.IdQuadroColunaPara
                && x.DataCriacao == cardLog.DataCriacao
                && x.DataMovimentacao == cardLog.DataMovimentacao);

            IAsyncCursor<CardLog> log = await _mongoDbContext.CardLogs.FindAsync(filter);

            return log.Any();
        }

        public async Task AddCardLog(CardLog cardLog)
        {
            await _mongoDbContext.CardLogs.InsertOneAsync(cardLog);
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
