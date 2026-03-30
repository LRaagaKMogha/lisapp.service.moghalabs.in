using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Service.Core.SelfHosting
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Service starting...");

                var startup = new ServiceStartup(_config);
                startup.StartupAPI();

                // Just wait until service stops
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in service execution");
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service stopping...");

            // Optional cleanup
            GC.Collect();

            await base.StopAsync(stoppingToken);
        }
    }
}
