using BeeFor.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeeFor.Application.Interfaces
{
    public interface IIntegraProjetoJiraAppService
    {
        Task ObterProjetosJiraBaseAuthentication(ConfiguracaoIntegracaoViewModel configuracaoIntegracaoViewModel);
    }
}
