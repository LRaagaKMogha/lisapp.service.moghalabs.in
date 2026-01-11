using AspNetCoreRateLimit;
using AutoMapper;
using Dev.IRepository;
using Dev.IRepository.Audit;
using Dev.IRepository.FrontOffice;
using Dev.IRepository.Inventory;
using Dev.IRepository.Inventory.Report;
using Dev.IRepository.Master;
using Dev.IRepository.PatientInfo;
using Dev.IRepository.Samples;
using Dev.IRepository.UserManagement;
using Dev.Repository;
using Dev.Repository.Audit;
using Dev.Repository.FrontOffice;
using Dev.Repository.FrontOffice.ReferrerWiseDue;
using Dev.Repository.Integration.externalservices;
using Dev.Repository.Inventory;
using Dev.Repository.Inventory.Report;
using Dev.Repository.Master;
using Dev.Repository.PatientInfo;
using Dev.Repository.Samples;
using Dev.Repository.UserManagement;
using DEV.API.SERVICE;
using DEV.API.SERVICE.InvoiceGeneration;
using DEV.API.SERVICE.Shared;
using DEV.Common;
using Service.Model.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var config = builder.Configuration;
ConfigurationHelper.InitializeConfiguration(config); 

// BasePath
var basePath = builder.Configuration["BasePath"] ?? string.Empty;

// Logging
string logPath = config["APIConfig:LogLocation"];
int.TryParse(config["APIConfig:FileSizeMB"], out int fileSizeMb);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(
        path: Path.Combine(logPath, $"log-{DateTime.Now:ddMMyyyy}.txt"),
        retainedFileCountLimit: null,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 1024 * 1024 * fileSizeMb)
    .CreateLogger();

builder.Host.UseSerilog();

// CORS
var policyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, builder =>
    {
        builder.WithOrigins(config["X-Frame-Options"])
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Swagger + API Key Security
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LIMS SERVICE", Version = "v1" });

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "ApiKey must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference {
              Type = ReferenceType.SecurityScheme, Id = "ApiKey" } }, new List<string>() }
    });
});

// JWT Authentication
var jwtKeyValue = config["JWT:Key"];
if (string.IsNullOrEmpty(jwtKeyValue))
    throw new Exception("JWT:Key is missing in configuration");

var jwtKey = Encoding.UTF8.GetBytes(config["JWT:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JWT:Issuer"],
        ValidAudience = config["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
    };
});

builder.Services.AddAuthorization();

// IP Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(config.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

// AutoMapper and DTO mapping
builder.Services.AddSingleton<IMapper>(AutoMapperConfiguration.Configure());
DtoMappingRegistry.RegisterMappingsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

// Dependency Injection
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(
    EncryptionHelper.Decrypt(config.GetConnectionString(ConfigKeys.DefaultConnection)) + ";MultipleActiveResultSets=True;"));

// Register your services here
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddTransient<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddTransient<IClientMasterRepository, ClientMasterRepository>();
builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<IManageSampleRepository, ManageSampleRepository>();
builder.Services.AddTransient<IResultRepository, ResultRepository>();
builder.Services.AddTransient<IMasterRepository, MasterRepository>();
builder.Services.AddTransient<IFrontOfficeRepository, FrontOfficeRepository>();
builder.Services.AddTransient<IPatientInfoRepository, PatientInfoRepository>();
builder.Services.AddTransient<IPatientDueRepository, PatientDueRepository>();
builder.Services.AddTransient<ISampleRepository, SampleRepository>();
builder.Services.AddTransient<IMethodRepository, MethodRepository>();
builder.Services.AddTransient<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddTransient<IRouteRepository, RouteRepository>();
builder.Services.AddTransient<IPatientReportRepository, PatientReportRepository>();
builder.Services.AddTransient<ICommonRepository, CommonRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<ITariffMasterRepository, TariffMasterRepository>();
builder.Services.AddTransient<ITestRepository, TestRepository>();
builder.Services.AddTransient<IMicrobiologyMasterRepository, MicrobiologyMasterRepository>();
builder.Services.AddTransient<IPhysicianRepository, PhysicianRepository>();
builder.Services.AddTransient<IWorkListRepository, WorkListRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IExternalRepository, ExternalRepository>();
builder.Services.AddTransient<IEditBillingRepository, EditBillingRepository>();
builder.Services.AddTransient<IDashBoardRepository, DashBoardRepository>();
builder.Services.AddTransient<IArchiveRepository, ArchiveRepository>();
builder.Services.AddTransient<IICMRResponseRepository, ICMRResponseRepository>();
builder.Services.AddTransient<IIPSettingMasterRepository, IPSettingMasterRepository>();
builder.Services.AddTransient<IRCMasterRepository, RCMasterRepository>();
builder.Services.AddTransient<IProductMasterRepository, ProductMasterReposistory>();
builder.Services.AddTransient<ISupplierMasterRepository, SupplierMasterReposistory>();
builder.Services.AddTransient<IManufacturerMasterRepository, ManufacturerMasterRepository>();
builder.Services.AddTransient<IMultiPriceListRepository, MultiPriceListRepository>();
builder.Services.AddTransient<IPurchaseOrderReposistory, PurchaseOrderReposistory>();
builder.Services.AddTransient<IUnitRepository, UnitRepository>();
builder.Services.AddTransient<IInventoryReportRepository, InventoryReportRepository>();
builder.Services.AddTransient<IGRNMasterReposistory, GRNMasterReposistory>();
builder.Services.AddTransient<IspecializationRepository, specializationRepository>();
builder.Services.AddTransient<IStockUploadReposistory, StockUploadReposistory>();
builder.Services.AddTransient<IGRNReturnReposistory, GRNReturnReposistory>();
builder.Services.AddTransient<IConsumptionMappingRepositoty, ConsumptionMappingRepositoty>();
builder.Services.AddTransient<IReagentOpeningStockRepositoty, ReagentOpeningStockRepositoty>();
builder.Services.AddTransient<IStockCorrectionRepositoty, StockCorrectionRepositoty>();
builder.Services.AddTransient<IOPDPatientRepository, OPDPatientRepository>();
builder.Services.AddTransient<IVitalSignRepository, VitalSignRepository>();
builder.Services.AddTransient<IContainerRepository, ContainerRepository>();
builder.Services.AddTransient<IMainDepartmentRepository, MainDepartmentRepository>();
builder.Services.AddTransient<IPackRepository, PackRepository>();
builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddTransient<ITermsRepository, TermsRepository>();
builder.Services.AddTransient<ISubtestheaderRepository, SubtestheaderRepository>();
builder.Services.AddTransient<ITaxRepository, TaxRepository>();
builder.Services.AddTransient<IPharmacyRepository, PharmacyRepository>();
builder.Services.AddTransient<IlocationMasterRepository, locationMasterRepository>();
builder.Services.AddTransient<ITitleRepository, TitleRepository>();
builder.Services.AddTransient<IAnaParamRepository, AnaParamRepository>();
builder.Services.AddTransient<IVendorMasterRepository, VendorMasterRepository>();
builder.Services.AddTransient<IProcessingbranchRepository, ProcessingbranchRepository>();
builder.Services.AddTransient<IExternalAPIRepository, ExternalAPIRepository>();
builder.Services.AddTransient<IQcmasterRepository, QcmasterRepository>();
builder.Services.AddTransient<IDocVsServiceMapRepository, DocVsServiceMapRepository>();
builder.Services.AddTransient<IInsuranceRepository, InsuranceRepository>();
builder.Services.AddTransient<IQcresultentryRepository, QcresultentryRepository>();
builder.Services.AddTransient<IAnalyzerMasterRepository, AnalyzerMasterRepository>();
builder.Services.AddTransient<IquotationRepository, quotationRepository>();
builder.Services.AddTransient<IServiceOrderRepository, ServiceOrderRepository>();
builder.Services.AddTransient<ICommentRepository, CommentMasterRepository>();
builder.Services.AddTransient<ICommericalRepository, CommericalMasterRepository>();
builder.Services.AddTransient<IIntegrationRepository, IntegrationRepository>();
builder.Services.AddTransient<IAuditRepository, AuditRepository>();
builder.Services.AddTransient<BloodBankService>();
builder.Services.AddTransient<IFinanceIntegrationRepository, FinanceIntegrationRepository>();
builder.Services.AddTransient<IAssetManagementRepository, AssetManagementRepository>();
builder.Services.AddTransient<IStockAlertRepository, StockAlertRepository>();
builder.Services.AddTransient<IStoreMasterRepository, StoreMasterRepository>();
builder.Services.AddTransient<ICollectionDetailsRepository, CollectionDetailsRepository>();
builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
builder.Services.AddTransient<IStockReportReposistory, StockReportRepository>();
builder.Services.AddTransient<IInventoryDashBoardRepository, InventoryDashBoardRepository>();
builder.Services.AddTransient<ICommonProductSupplierMappingRepository, CommonProductSupplierMappingRepository>();
builder.Services.AddTransient<IVenueVsMenuRepository, VenueVsMenuRepository>();
builder.Services.AddTransient<IScrollTextMasterRepository, ScrollTextMasterRepository>();
builder.Services.AddTransient<ICommonConfigurationRepository, CommonConfigurationRepository>();
builder.Services.AddTransient<IOPDDashBoardRepository, OPDDashBoardRepository>();
builder.Services.AddTransient<ITestTemplateMasterRepository, TestTemplateMasterRepository>();
builder.Services.AddTransient<IStoreProductMappingRepository, StoreProductMappingRepository>();
builder.Services.AddTransient<IDiseaseRepository, DiseaseRepository>();
builder.Services.AddTransient<IAllergyRepository, AllergyRepository>();
builder.Services.AddTransient<ISlidePrintingRepository, SlidePrintingRepository>();
builder.Services.AddTransient<IClientBranchSamplePickupRepository, ClientBranchSamplePickupRepository>();
builder.Services.AddTransient<IFranchisorRepository, FranchisorRepository>();
builder.Services.AddTransient<IReferrerWiseDueRepository, ReferrerWiseDueRepository>();
builder.Services.AddTransient<IOutSourceAPIRepository, OutSourceAPIRepository>();

// MVC and Compatibility
//builder.Services.AddControllers().AddMvcOptions(options => { }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
builder.Services.AddControllers();

var app = builder.Build();

if (!string.IsNullOrEmpty(basePath))
{
    app.UsePathBase(basePath);
    app.Use((context, next) =>
    {
        context.Request.PathBase = basePath;
        return next();
    });
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors(policyName);
app.UseIpRateLimiting();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    var swaggerBasePath = string.IsNullOrEmpty(basePath)
        ? "/swagger/v1/swagger.json"
        : $"{basePath}/swagger/v1/swagger.json";

    c.SwaggerEndpoint(swaggerBasePath, "LIMS SERVICE");
    c.RoutePrefix = "swagger";
});


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<SecurityMiddleWare>();
app.MapControllers();

app.Run();
