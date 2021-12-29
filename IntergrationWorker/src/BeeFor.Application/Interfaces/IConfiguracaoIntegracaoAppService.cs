using BeeFor.Application.Models;
using BeeFor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeFor.Application.Interfaces
{
    public interface IConfiguracaoIntegracaoAppService
    {
        Task<List<ConfiguracaoIntegracaoViewModel>> ConfiguracaoIntegracaoLista();
    }
}
