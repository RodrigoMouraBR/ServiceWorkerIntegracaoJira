using BeeFor.Data.Context;
using BeeFor.Domain.Entities;
using BeeFor.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeeFor.Data.Repositories
{
    public class ConfiguracaoIntegracaoRepository : IConfiguracaoIntegracaoRepository
    {
        private readonly BeeForContext _context;
        public ConfiguracaoIntegracaoRepository(BeeForContext context)
        {
            _context = context;
        }
        public async Task<List<ConfiguracaoIntegracao>> ConfiguracaoIntegracaoLista()
        {
            return await _context.ConfiguracaoIntegracoes.AsNoTracking().ToListAsync();
        }
    }
}
