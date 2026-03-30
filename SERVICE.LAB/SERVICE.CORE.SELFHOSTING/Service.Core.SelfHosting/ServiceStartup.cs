using Service.Common.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service.Core.SelfHosting
{
    public class ServiceStartup
    {
        private readonly IConfiguration _config;
        private readonly Logger _logger;

        public ServiceStartup(IConfiguration config)
        {
            _config = config;
            _logger = new Logger(config);
        }

        public async Task StartupAPI()
        {
            try
            {
                _logger.LogWrite("StartupAPI START");

                var port1 = _config["WinService:PortNo"];

                _logger.LogWrite("Port value: " + port1);

                var builder = WebApplication.CreateBuilder();

                builder.Services.AddControllers();
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });

                var app = builder.Build();

                app.UseCors("AllowAll");
                app.MapControllers();

                var port = _config["WinService:PortNo"];

                var url = $"http://*:{port}";
                _logger.LogWrite("Binding URL: " + url);

                app.Urls.Clear();
                app.Urls.Add(url);

                _logger.LogWrite("Before RunAsync");

                await app.RunAsync();   // 👈 THIS is where failure happens

                _logger.LogWrite("After RunAsync"); // will NEVER hit normally
            }
            catch (Exception ex)
            {
                _logger.LogWrite("StartupAPI ERROR: " + ex.ToString());
                throw;
            }
        }
    }
}