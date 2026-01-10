using System.Text.Json.Serialization;
using BloodBankManagement.AutoServices;
using BloodBankManagement.Helpers;
using BloodBankManagement.Services.BloodBankInventories;
using BloodBankManagement.Services.BloodBankPatients;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Services.BloodSampleInventories;
using BloodBankManagement.Services.BloodSampleResults;
using BloodBankManagement.Services.SampleReceiving;
using BloodBankManagement.Services.StartupServices;
using BloodBankManagement.Services.StandardPatinetReport;
using Serilog;
using Shared;
using BloodBankManagement.Services.Downloads;
using BloodBankManagement.Services.Reports;
using DEV.Common;
using Serilog.Filters;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using BloodBankManagement.Services.Integration;
using Microsoft.Data.SqlClient;
using Shared.Audit;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;

    services.AddControllers();
    services.AddHttpContextAccessor();
    services.AddDbContext<BloodBankDataContext>();
    services.AddCors();
    var configurations = new Dictionary<string, string>();
    configurations.Add("MKey", builder.Configuration.GetSection("MKey").Value);
    configurations.Add("Salt", builder.Configuration.GetSection("Salt").Value);
    ConfigurationHelper.InitializeBBConfiguration(configurations);
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var problems = new CustomBadRequest(context);
            return new BadRequestObjectResult(problems);
        };
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    DtoMappingRegistry.RegisterMappingsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    services.AddScoped<IDbConnection>(sp => new SqlConnection(EncryptionHelper.Decrypt(builder.Configuration.GetConnectionString("WebApiDatabase")) + ";MultipleActiveResultSets=True;"));
    services.AddScoped<IAuditService, AuditService>();
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    services.AddScoped<IBloodBankRegistrationService, BloodBankRegistrationService>();
    services.AddScoped<ISampleReceivingService, SampleReceivingService>();
    services.AddScoped<IBloodBankInventoryService, BloodBankInventoryService>();
    services.AddScoped<IBloodSampleResultService, BloodSampleResultService>();
    services.AddScoped<IBloodSampleInventoriesService, BloodSampleInventoriesService>();
    services.AddScoped<IBloodBankPatientService, BloodBankPatientService>();
    services.AddScoped<IDownloadService, DownloadService>();
    services.AddScoped<IReportsService, ReportsService>();
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddHostedService<ConsumeScopedServiceHostedService>();
    services.AddHostedService<BarCodeResetHostedService>();
    services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
    services.AddSingleton<StartupService>();
    services.AddScoped<IStandardPatientReportService, StandardPatientReportService>();
    services.AddScoped<IBBPatientReport, BBPatientReport>();
    services.AddScoped<IIntegrationService, IntegrationService>();


    services.AddMemoryCache();
    services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
    services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    services.AddInMemoryRateLimiting();
}
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();
var app = builder.Build();

{
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore"))
    .CreateLogger();

}
{
    app.UseSwagger();
    app.UseIpRateLimiting();
    app.UseSwaggerUI();
}
{
    app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseMiddleware<JwtMiddleware>();
    app.MapControllers();
    app.Run();
}
