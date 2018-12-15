using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NStore.Web.Framework
{
    public class UsersProcessorHostedService : BackgroundService
    {
        private readonly Random _random = new Random();
        private readonly ILogger<UsersProcessorHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UsersProcessorHostedService(ILogger<UsersProcessorHostedService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Processing...");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var page = _random.Next(1, 5);
                    _logger.LogInformation($"Page: {page}");
                    var cache = scope.ServiceProvider.GetService<IMemoryCache>();
                    var key = $"users:{page}";

                    var users = cache.Get<IEnumerable<UserData>>(key);
                    if (users != null)
                    {
                        _logger.LogInformation("Found in cache.");
                    }
                    else
                    {
                        var reqResClient = scope.ServiceProvider.GetService<IReqResClient>();
                        users = await reqResClient.BrowseAsync(page);
                        cache.Set(key, users);
                        _logger.LogInformation("Added to cache.");
                    }
                    
                    _logger.LogInformation("Users: " +
                                           $"{string.Join(", ", users.Select(u => u.FirstName))}");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}