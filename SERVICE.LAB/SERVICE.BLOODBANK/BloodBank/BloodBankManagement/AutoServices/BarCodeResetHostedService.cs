 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.AutoServices
{
    public class BarCodeResetHostedService : BackgroundService
    {
        private const int generalDelay = 1000 * 3600 * 1; // 1 hr

        private readonly ILogger<BarCodeResetHostedService> _logger;
        public IServiceProvider Services { get; }

        public BarCodeResetHostedService(IServiceProvider services,
            ILogger<BarCodeResetHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                DateTime DateAsOf2AMInMorning = DateTime.Now.Date.AddHours(2);
                _logger.LogInformation("BarCode Date Values --> {CurrentTime} --- {DateAsOf2AMInMorning} ---> {ComparisonResult}", DateTime.Now.ToString(), DateAsOf2AMInMorning.ToString(), DateTime.Now < DateAsOf2AMInMorning);
                if (DateTime.Now < DateAsOf2AMInMorning)
                {
                    using (var scope = Services.CreateScope())
                    {
                        _logger.LogInformation("BackGround service for RESTARTING the BarCodeId Sequence.");
                        var context = scope.ServiceProvider.GetRequiredService<BloodBankDataContext>();
                        //context.Database.ExecuteSqlRaw("ALTER SEQUENCE BarCodeId RESTART WITH 5001");
                    }
                }
                //Do Work
                await Task.Delay(generalDelay, stoppingToken);

            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}