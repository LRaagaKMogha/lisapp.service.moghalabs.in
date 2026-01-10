using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QC.AutoServices
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private const int generalDelay = 1 * 10 * 1000; // 10 seconds

        private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

        public ConsumeScopedServiceHostedService(IServiceProvider services,
            ILogger<ConsumeScopedServiceHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service running.");
            _logger.LogInformation("Each Execution");
            await Task.Delay(generalDelay, stoppingToken);
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWork(cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}