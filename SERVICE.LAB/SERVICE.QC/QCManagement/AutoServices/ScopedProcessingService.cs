using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QC.Services.StartupServices;


namespace QC.AutoServices
{
    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly StartupService startupService;
        //private const int generalDelay = 1000 * 3600 * 6; // 6 hrs
        private const int generalDelay = 1000 * 600 * 1; // 10 mins 
        //private const int generalDelay = 1000 * 60 ; // 1 minute

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, StartupService _startupService)
        {
            _logger = logger;
            this.startupService = _startupService;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("Scoped Processing Service is working. Count: {Count}", executionCount);
                //TODO: startup code
                await Task.Delay(generalDelay, cancellationToken);
            }
        }
    }
}