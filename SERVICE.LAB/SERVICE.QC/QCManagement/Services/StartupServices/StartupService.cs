using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QC.Helpers;
using Shared;

namespace QC.Services.StartupServices
{
    public class StartupService
    {
        private readonly QCDataContext dataContext;
        private readonly IJwtUtils jwtUtils;
        private readonly IConfiguration Configuration;
        private readonly ILogger<StartupService> _logger;


        public StartupService(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<StartupService> logger)
        {
            this.Configuration = configuration;
            this._logger = logger;
            dataContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<QCDataContext>();
            this.jwtUtils = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IJwtUtils>();

            startupCode();
        }
        public async Task<bool> startupCode()
        {
            _logger.LogInformation("start of QC");
            return true;
        }
        
    }
}