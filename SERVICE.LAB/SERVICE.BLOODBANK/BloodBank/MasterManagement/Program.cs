using System.Data;
using System.Text.Json.Serialization;
using AspNetCoreRateLimit;
using DEV.Common;
using MasterManagement.Helpers;
using MasterManagement.Services.Lookups;
using MasterManagement.Services.Nurses;
using MasterManagement.Services.Products;
using MasterManagement.Services.Tariffs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using Serilog.Filters;
using Shared;
using Shared.Audit;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    services.AddControllers();
    services.AddHttpContextAccessor();
    services.AddDbContext<MasterDataContext>();
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
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ILookupService, LookupService>();
    services.AddScoped<INurseService, NurseService>();
    services.AddScoped<ITariffService, TariffService>();
    services.AddScoped<IJwtUtils, JwtUtils>();
}
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

var app = builder.Build();

{
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    //.Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore"))
    .CreateLogger();
}
{
    app.UseSwagger();
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
    app.UseIpRateLimiting();
    app.Run();
}

