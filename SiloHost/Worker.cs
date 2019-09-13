using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace SiloHost
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ISiloHost _host;
        private readonly IServiceProvider _serviceProvider;
        public Worker(IServiceProvider serviceProvider,ILogger<Worker> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(!stoppingToken.IsCancellationRequested)
            {
                _host = _serviceProvider.GetService<ISiloHost>();
                await _host.StartAsync();
                _logger.LogInformation("ISiloHost start");
            }
        }
        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            await _host.StopAsync();
        }
    }
}
