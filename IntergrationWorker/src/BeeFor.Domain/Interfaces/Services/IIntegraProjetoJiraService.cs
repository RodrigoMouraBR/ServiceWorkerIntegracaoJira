using BeeFor.Domain.Entities;
using System.Threading.Tasks;

namespace BeeFor.Domain.Interfaces.Services
{
    public interface IIntegraProjetoJiraService
    {
        Task ObterProjetosJiraBaseAuthentication(ConfiguracaoIntegracao listConfiguracaoIntegracao);
    }
}
