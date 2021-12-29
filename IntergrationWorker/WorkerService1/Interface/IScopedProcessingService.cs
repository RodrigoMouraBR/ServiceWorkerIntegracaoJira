using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1.Interface
{
    public interface IScopedProcessingService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
