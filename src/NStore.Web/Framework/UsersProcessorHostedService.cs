using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NStore.Web.Framework
{
    public class UsersProcessorHostedService : BackgroundService
    {
        private readonly ILogger<UsersProcessorHostedService> _logger;
        private readonly IReqResClient _reqResClient;

        public UsersProcessorHostedService(ILogger<UsersProcessorHostedService> logger,
            IReqResClient reqResClient)
        {
            _logger = logger;
            _reqResClient = reqResClient;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Processing...");
                var users = await _reqResClient.BrowseAsync();
                _logger.LogInformation("Users: " + 
                $"{string.Join(", ", users.Select(u => u.FirstName))}");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}