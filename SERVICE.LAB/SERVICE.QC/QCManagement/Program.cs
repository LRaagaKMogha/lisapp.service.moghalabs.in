using System.Text.Json.Serialization;
using Serilog;
using Shared;
using Serilog.Filters;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using QC.Helpers;
using DEV.Common;
using QC.AutoServices;
using QC.Services.StartupServices;
using QCManagement.Services.ControlMaster;
using QCManagement.Services.QCResultEntry;
using QCManagement.Services.Scheduler;
using QCManagement.Services.Reagent;
using QCManagement.Services.MediaInventory;
using QCManagement.Services.StrainMaster;
using QCManagement.Services.StrainInventory;
using QCManagement.Services.StrainMediaMapping;
using QCManagement.Services.MicroQCMaster;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;

    services.AddControllers();
    services.AddHttpContextAccessor();
    services.AddDbContext<QCDataContext>();
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
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    services.AddScoped<ISchedulerService, SchedulerService>();
    services.AddScoped<IControlMasterService, ControlMasterService>();
    services.AddScoped<IQCResultEntryService, QCResultEntryService>();
    services.AddScoped<IReagentService, ReagentService>();
    services.AddScoped<IMediaInventoryService, MediaInventoryService>();
    services.AddScoped<IStrainMasterService, StrainMasterService>();
    services.AddScoped<IStrainInventoryService, StrainInventoryService>();
    services.AddScoped<IStrainMediaMappingService, StrainMediaMappingService>();
    services.AddScoped<IMicroQCMasterService, MicroQCMasterService>();
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddHostedService<ConsumeScopedServiceHostedService>();
    services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
    services.AddSingleton<StartupService>();


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
