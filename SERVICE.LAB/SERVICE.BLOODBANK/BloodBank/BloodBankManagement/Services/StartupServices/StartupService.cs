using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.BloodBankRegistrations;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared;

namespace BloodBankManagement.Services.StartupServices
{
    public class StartupService
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IJwtUtils jwtUtils;
        private readonly IConfiguration Configuration;
        private readonly IBloodBankRegistrationService bloodBankRegistration;
        private readonly ILogger<StartupService> _logger;


        public StartupService(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<StartupService> logger)
        {
            this.Configuration = configuration;
            this._logger = logger;
            dataContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<BloodBankDataContext>();
            bloodBankRegistration = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IBloodBankRegistrationService>();
            this.jwtUtils = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IJwtUtils>();

            GetBloodBankRelatedTests();
        }
        public async Task<bool> GetBloodBankRelatedTests()
        {
            _logger.LogInformation("start of GetBloodBankRelatedTests");
            var tests = await this.bloodBankRegistration.GetBloodBankTests();
            if (!tests.IsError) GlobalConstants.Tests = tests.Value;
            _logger.LogInformation("after bloodbank tests {TestsCount}", GlobalConstants.Tests.Count);
            var subtests = await this.bloodBankRegistration.GetBloodBankSubTests();
            if (!subtests.IsError) GlobalConstants.SubTests = subtests.Value;
            _logger.LogInformation("after bloodbank subtests {SubTestsCount}", GlobalConstants.SubTests.Count);
            var groups = await this.bloodBankRegistration.GetBloodBankGroups();
            if (!groups.IsError) GlobalConstants.Groups = groups.Value;
            _logger.LogInformation("after bloodbank groups {GroupsCount}", GlobalConstants.Groups.Count);

            GlobalConstants.BloodGroupingRHId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.BloodGroupingRH).TestNo;
            _logger.LogInformation("1");
            GlobalConstants.BloodGroupingRHAutoId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.BloodGroupingRHAuto).TestNo;
            _logger.LogInformation("1.1");
            // GlobalConstants.BabyBloodGroupingRHAutoId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.BabyBloodGroupingRHAuto).TestNo;
            // _logger.LogInformation("1.1.1");
            GlobalConstants.ABOConfirmationId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.ABOConfirmation).TestNo;
            _logger.LogInformation("1.2");
            // GlobalConstants.BabyABOConfirmationId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.BabyABOConfirmation).TestNo;
            // _logger.LogInformation("1.2.1");
            GlobalConstants.AntibodyScreeningId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.AntibodyScreening).TestNo;
            _logger.LogInformation("2");
            GlobalConstants.CrossMatchingXMId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.CrossMatchingXM).TestNo;
            GlobalConstants.CrossMatchingManualXMId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.CrossMatchingManualXM).TestNo;
            GlobalConstants.CrossMatchingImmediateSpinXMId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.CrossMatchingImmediateSpinXM).TestNo;
            _logger.LogInformation("3");
            GlobalConstants.AntibodyIdentifiedId = GlobalConstants.Tests.First(x => x.TestShortName == Constants.AntibodyIdentified).TestNo;
            //GlobalConstants.AntibodyIdentifiedId2 = GlobalConstants.Tests.First(x => x.TestShortName == Constants.AntibodyIdentified2).TestNo;
            _logger.LogInformation("4");
            GlobalConstants.CrossMatchingInterpretationTestId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.CrossMatchingXMId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            GlobalConstants.CrossMatchingManualInterpretationTestId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.CrossMatchingManualXMId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            GlobalConstants.CrossMatchingImmediateSpinInterpretationTestId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.CrossMatchingImmediateSpinXMId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            _logger.LogInformation("5");
            GlobalConstants.BloodGroupingRHInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.BloodGroupingRHId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            _logger.LogInformation("6");
            GlobalConstants.BloodGroupingRHAutoInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.BloodGroupingRHAutoId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            _logger.LogInformation("6.1");
            GlobalConstants.ABOConfirmationInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.ABOConfirmationId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            _logger.LogInformation("6.2");
            // GlobalConstants.BabyBloodGroupingRHAutoInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.BabyBloodGroupingRHAutoId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            // _logger.LogInformation("6.3");
            // GlobalConstants.BabyABOConfirmationInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.BabyABOConfirmationId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            // _logger.LogInformation("6.4");

            GlobalConstants.AntibodyScreeningInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.AntibodyScreeningId && x.SubTestCode == Constants.INTERPRETATION).SubTestNo;
            _logger.LogInformation("identified Id --> {AntiBodyId} - {SubTestsCount}", GlobalConstants.AntibodyIdentifiedId, GlobalConstants.SubTests.Count(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId && x.SubTestCode == "ANTIBODIES-MULTI"));
            _logger.LogInformation("identified Id2 --> {AntiBodyId2} - {SubTestsCount2}", GlobalConstants.AntibodyIdentifiedId2, GlobalConstants.SubTests.Count(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId2 && x.SubTestCode == "ANTIBODIES-MULTI"));
            _logger.LogInformation("test names --> {TestNames}", string.Join(",", GlobalConstants.SubTests.Where(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId).Select(x => x.SubTestCode).ToArray()));
            _logger.LogInformation("test names --> {TestNames2}", string.Join(",", GlobalConstants.SubTests.Where(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId2).Select(x => x.SubTestCode).ToArray()));
            GlobalConstants.AntibodyIdentifiedInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId && x.SubTestCode == "ANTIBODIES-MULTI").SubTestNo;
            GlobalConstants.ColdAntibodyIdentifiedInterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId && x.SubTestCode == "CRANTI-MULTI")?.SubTestNo ?? 0;
            _logger.LogInformation("antibody interpretation --> {AntibodyInterpretationId} - {ColdAntibodyInterpretationId}", GlobalConstants.AntibodyIdentifiedInterpretationId, GlobalConstants.ColdAntibodyIdentifiedInterpretationId);
            //GlobalConstants.AntibodyIdentified2InterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId2 && x.SubTestCode == "ANTIBODIES-MULTI").SubTestNo;
            //GlobalConstants.ColdAntibodyIdentified2InterpretationId = GlobalConstants.SubTests.First(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId2 && x.SubTestCode == "CRANTI-MULTI")?.SubTestNo ?? 0;
            _logger.LogInformation("antibody interpretation2 --> {AntibodyInterpretationId2} - {ColdAntibodyInterpretationId2}", GlobalConstants.AntibodyIdentified2InterpretationId, GlobalConstants.ColdAntibodyIdentified2InterpretationId);

            _logger.LogInformation("before masterdata");

            await this.MasterData();
            _logger.LogInformation("after masterdata");
            return true;
        }
        public async Task MasterData()
        {
            _logger.LogInformation("start of masterData");
            string masterUrl = this.Configuration.GetValue<string>(Constants.MasterUrl);
            bool isCertificateAvailable = this.Configuration.GetValue<bool>("IsCertificateAvailable");
            _logger.LogInformation("MasterUrl {url}", masterUrl);
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage requestMessage, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors sslErrors) =>
            {
                _logger.LogInformation("Requested URI: {uri}", requestMessage.RequestUri);
                _logger.LogInformation("Effective date: {date}", certificate?.GetEffectiveDateString());
                _logger.LogInformation("Exp date: {date}", certificate?.GetExpirationDateString());
                _logger.LogInformation("Issuer: {issuer}", certificate?.Issuer);
                _logger.LogInformation("Subject: {subject}", certificate?.Subject);

                // Based on the custom logic it is possible to decide whether the client considers certificate valid or not
                _logger.LogInformation("Errors: {errors}", sslErrors);

                return sslErrors == SslPolicyErrors.None || !isCertificateAvailable;
            };
            var httpClient = new HttpClient(httpClientHandler);
            try
            {
                var token = jwtUtils.GenerateToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(masterUrl + "/Lookups");
                var lookupsstring = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("response details {statusCode} -> {requestMessage} ", response.StatusCode, response.RequestMessage);
                var lookups = JsonConvert.DeserializeObject<ServiceResponse<List<Lookup>>>(lookupsstring);
                GlobalConstants.Lookups.AddRange(lookups?.Data ?? new List<Lookup>());
                _logger.LogInformation("lookups {lookupCount}", GlobalConstants.Lookups.Count);
                var response1 = await httpClient.GetAsync(masterUrl + "/Products");
                var lookupsstring1 = await response1.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<ServiceResponse<List<Product>>>(lookupsstring1);
                GlobalConstants.Products.AddRange(products?.Data ?? new List<Product>());
                _logger.LogInformation("products {productCount}", GlobalConstants.Products.Count);
                var response2 = await httpClient.GetAsync(masterUrl + "/Tariffs");
                var lookupsstring2 = await response2.Content.ReadAsStringAsync();
                var tariffs = JsonConvert.DeserializeObject<ServiceResponse<List<Tariff>>>(lookupsstring2);
                GlobalConstants.Tariffs.AddRange(tariffs?.Data ?? new List<Tariff>());
                _logger.LogInformation("Tariffs {tariffsCount}", GlobalConstants.Tariffs.Count);
            }
            catch (Exception exp)
            {
                _logger.LogInformation("exception {ExceptionMessage}", exp?.InnerException?.Message);
            }
            GlobalConstants.RedCellCategoryId = (GlobalConstants.Lookups.FirstOrDefault(x => x.Code == "RED")?.Identifier).GetValueOrDefault();
            GlobalConstants.RedCellsProducts = GlobalConstants.Products.Where(x => x.CategoryId == GlobalConstants.RedCellCategoryId).ToList();

        }
        public async Task<ErrorOr<bool>> CheckSRDData()
        {
            try
            {
                var tests = await this.bloodBankRegistration.GetBloodBankTests();
                if (!tests.IsError)
                {
                    if (GlobalConstants.Tests.Any() && tests.Value.Any())
                    {
                        if (DateTime.Compare(GlobalConstants.Tests.Max(x => x.ModifiedOn.GetValueOrDefault()), tests.Value.Max(x => x.ModifiedOn.GetValueOrDefault())) < 0)
                        {
                            GlobalConstants.Tests = tests.Value;
                        }
                    }
                }
                var subtests = await this.bloodBankRegistration.GetBloodBankSubTests();
                if (!subtests.IsError)
                {
                    if (GlobalConstants.SubTests.Any() && subtests.Value.Any())
                    {
                        if (DateTime.Compare(GlobalConstants.SubTests.Max(x => x.ModifiedOn.GetValueOrDefault()), subtests.Value.Max(x => x.ModifiedOn.GetValueOrDefault())) < 0)
                        {
                            GlobalConstants.SubTests = subtests.Value;
                        }
                    }
                }
                string masterUrl = this.Configuration.GetValue<string>(Constants.MasterUrl);
                bool isCertificateAvailable = this.Configuration.GetValue<bool>("IsCertificateAvailable");
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage requestMessage, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors sslErrors) =>
                {
                    return sslErrors == SslPolicyErrors.None || !isCertificateAvailable;
                };
                var httpClient = new HttpClient(httpClientHandler);
                var token = jwtUtils.GenerateToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(masterUrl + "/Lookups");
                var lookupsstring = await response.Content.ReadAsStringAsync();
                var lookups = JsonConvert.DeserializeObject<ServiceResponse<List<Lookup>>>(lookupsstring);
                var lookupData = lookups?.Data ?? new List<Lookup>();
                if (GlobalConstants.Lookups.Any() && lookupData.Any())
                {
                    if (DateTime.Compare(GlobalConstants.Lookups.Max(x => x.LastModifiedDateTime.GetValueOrDefault()), lookupData.Max(x => x.LastModifiedDateTime.GetValueOrDefault())) < 0)
                    {
                        GlobalConstants.Lookups.Clear();
                        GlobalConstants.Lookups.AddRange(lookupData);
                    }
                }

                var response1 = await httpClient.GetAsync(masterUrl + "/Products");
                var lookupsstring1 = await response1.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<ServiceResponse<List<Product>>>(lookupsstring1);
                var productsData = products?.Data ?? new List<Product>();
                if (GlobalConstants.Products.Any() && productsData.Any())
                {
                    if (DateTime.Compare(GlobalConstants.Products.Max(x => x.LastModifiedDateTime), productsData.Max(x => x.LastModifiedDateTime)) < 0)
                    {
                        GlobalConstants.Products.Clear();
                        GlobalConstants.Products.AddRange(productsData);
                    }
                }
                var response2 = await httpClient.GetAsync(masterUrl + "/Tariffs");
                var lookupsstring2 = await response2.Content.ReadAsStringAsync();
                var tariffs = JsonConvert.DeserializeObject<ServiceResponse<List<Tariff>>>(lookupsstring2);
                var tariffsData = tariffs?.Data ?? new List<Tariff>();
                if (GlobalConstants.Tariffs.Any() && tariffsData.Any())
                {
                    if (DateTime.Compare(GlobalConstants.Tariffs.Max(x => x.LastModifiedDateTime), tariffsData.Max(x => x.LastModifiedDateTime)) < 0)
                    {
                        GlobalConstants.Tariffs.Clear();
                        GlobalConstants.Tariffs.AddRange(tariffsData);
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogInformation("exception inside CheckSRDData {ExceptionMessage}", exp?.InnerException?.Message);
                return false;
            }
            return true;
        }
        public async Task<ErrorOr<bool>> CheckForExpiration()
        {
            var modifiedBy = 1;
            var modifiedByUserName = "System";
            var allowedStatuses = new List<string>() { "SampleReceived", "SampleProcessed", "ResultValidated", "InventoryAssigned" };
            var nonExpiredStatuses = HelperMethods.BloodSampleInventoryNonExpiryStatuses;
            var registrations = dataContext.BloodBankRegistrations.Where(x => allowedStatuses.Any(y => y == x.Status)).ToList();
            var expiredRegistations = new List<Int64>();
            var bloodSampleinventories = new List<BloodSampleInventory>();
            var inventories = new List<BloodBankInventory>();
            registrations.ForEach(x =>
            {
                if (x.SampleReceivedDateTime != null && x.SampleReceivedDateTime.Value.Date.AddDays(3) < DateTime.Now.Date)
                {
                    expiredRegistations.Add(x.RegistrationId);
                }
            });
            await this.bloodBankRegistration.UpdateRegistrationStatus(expiredRegistations, "Expired", modifiedBy, modifiedByUserName);
            expiredRegistations.ForEach(registrationId =>
            {
                bloodSampleinventories = dataContext.BloodSampleInventories.Where(x => x.RegistrationId == registrationId && nonExpiredStatuses.All(y => y != x.Status)).ToList();
                var inventoriesList = bloodSampleinventories.Select(x => x.InventoryId).Distinct().ToList();
                inventories = dataContext.BloodBankInventories.Where(x => inventoriesList.Any(y => y == x.Identifier)).ToList();
                bloodSampleinventories.ForEach(item =>
                {
                    item.Status = "Expired";
                    item.ModifiedBy = modifiedBy;
                    item.ModifiedByUserName = modifiedByUserName;
                    item.LastModifiedDateTime = DateTime.Now;
                    dataContext.BloodSampleInventories.Update(item);
                });
                var notAllowedStatuses = new List<string>() { "Issued", "Transfused", "ReturnedtoBSG", "Expired", "Disposed", "rejected" };
                for(var i = 0; i < inventories.Count; i++)
                {
                    var inventory = inventories[i];
                    if (!notAllowedStatuses.Any(y => y == inventory.Status))
                    {
                        inventory.Status = inventory.ExpirationDateAndTime <= DateTime.Now ? "Expired" : "available";
                        inventory.ModifiedBy = modifiedBy;
                        inventory.ModifiedByUserName = modifiedByUserName;
                        inventory.LastModifiedDateTime = DateTime.Now;
                        dataContext.BloodBankInventories.Update(inventory);
                        var inventoryTransaction = new BloodBankInventoryTransaction(inventory.Identifier, inventory.Status, inventory.Comments, inventory.ModifiedBy, inventory.ModifiedByUserName, inventory.LastModifiedDateTime);
                        this.dataContext.bloodBankInventoryTransactions.Add(inventoryTransaction);

                    }
                };
            });
            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<ErrorOr<bool>> UpdateInventoryStatus()
        {
            var modifiedBy = 1;
            var modifiedByUserName = "System";
            var inventories = await dataContext.BloodBankInventories.Where(x => x.Status == "Quarantine" || x.Status == "Returned").ToListAsync();
            for(var i = 0; i < inventories.Count; i++)
            {
                var inventory = inventories[i];
                if (inventory.LastModifiedDateTime.Date.AddDays(3) < DateTime.Now.Date)
                {
                    inventory.Status = inventory.ExpirationDateAndTime <= DateTime.Now ? "Expired" : "available";
                    inventory.ModifiedBy = modifiedBy;
                    inventory.ModifiedByUserName = modifiedByUserName;
                    inventory.LastModifiedDateTime = DateTime.Now;
                    dataContext.BloodBankInventories.Update(inventory);
                    var inventoryTransaction = new BloodBankInventoryTransaction(inventory.Identifier, inventory.Status, inventory.Comments, inventory.ModifiedBy, inventory.ModifiedByUserName, inventory.LastModifiedDateTime);
                    await this.dataContext.bloodBankInventoryTransactions.AddAsync(inventoryTransaction);
                }
            };
            await dataContext.SaveChangesAsync();

            try
            {
                var inventoryExpiryStatus = new List<string>() { "uncheck", "available", "Assigned", "OnHold" };
                var bloodSampleInventoryExpiryStatus = new List<string>() { "InventoryAssigned", "OnHold" };
                var expiredInventories = await dataContext.BloodBankInventories.Where(x => x.ExpirationDateAndTime <= DateTime.Now && inventoryExpiryStatus.Any(y => y == x.Status)).ToListAsync();
                var expiredInventoryIds = expiredInventories.Select(x => x.Identifier).ToList();
                var bloodSampleinventories = await dataContext.BloodSampleInventories.Where(x => expiredInventoryIds.Any(y => y == x.InventoryId) && bloodSampleInventoryExpiryStatus.Any(y => y == x.Status)).ToListAsync();
                bloodSampleinventories.ForEach(item =>
                {
                    item.Status = "Expired";
                    item.ModifiedBy = modifiedBy;
                    item.ModifiedByUserName = modifiedByUserName;
                    item.LastModifiedDateTime = DateTime.Now;
                    dataContext.BloodSampleInventories.Update(item);
                });
                for(var i = 0; i < expiredInventories.Count; i++)
                {
                    var inventory = expiredInventories[i];
                    inventory.Status = "Expired";
                    inventory.ModifiedBy = modifiedBy;
                    inventory.ModifiedByUserName = modifiedByUserName;
                    inventory.LastModifiedDateTime = DateTime.Now;
                    dataContext.BloodBankInventories.Update(inventory);
                    var inventoryTransaction = new BloodBankInventoryTransaction(inventory.Identifier, inventory.Status, inventory.Comments, inventory.ModifiedBy, inventory.ModifiedByUserName, inventory.LastModifiedDateTime);
                    await this.dataContext.bloodBankInventoryTransactions.AddAsync(inventoryTransaction);
                };
                await dataContext.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                _logger.LogInformation("Exception Message {ExceptionMessage}", exp.Message);
            }
            return true;
        }
    }
}
