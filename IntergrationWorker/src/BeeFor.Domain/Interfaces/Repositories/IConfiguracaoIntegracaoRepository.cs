using BeeFor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeeFor.Domain.Interfaces.Repositories
{
    public interface IConfiguracaoIntegracaoRepository
    {
        Task<List<ConfiguracaoIntegracao>> ConfiguracaoIntegracaoLista();
    }
}
