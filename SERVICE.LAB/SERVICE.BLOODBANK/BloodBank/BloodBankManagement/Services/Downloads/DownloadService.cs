using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.BloodBankInventories;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Services.StartupServices;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Services.Downloads
{
    public class DownloadService : IDownloadService
    {
        private readonly Dictionary<int, string> MonthMap = new Dictionary<int, string>
        {
            { 1, "A" },  // January
            { 2, "B" },  // February
            { 3, "C" },  // March
            { 4, "D" },  // April
            { 5, "E" },  // May
            { 6, "F" },  // June
            { 7, "G" },  // July
            { 8, "H" },  // August
            { 9, "I" },  // September
            { 10, "J" }, // October
            { 11, "K" }, // November
            { 12, "L" }  // December
        };
        private readonly IBloodBankRegistrationService registrationService;
        private readonly BloodBankDataContext dataContext;
        private readonly IBloodBankInventoryService inventoryService;
        private readonly ILogger<DownloadService> _logger;
        public DownloadService(BloodBankDataContext dataContext, IBloodBankRegistrationService _registrationService, IBloodBankInventoryService _inventoryService, ILogger<DownloadService> logger)
        {
            this.dataContext = dataContext;
            this.registrationService = _registrationService;
            this.inventoryService = _inventoryService;
            this._logger = logger;
        }


        public async Task<ErrorOr<Dictionary<string, List<string>>>> GenerateTransfusedLinesForEDI(DateTime startDate, DateTime endDate)
        {
            var bloodSampleInventories = await dataContext.BloodSampleInventories.Where(x => x.Status == "Transfused" && x.LastModifiedDateTime >= startDate && x.LastModifiedDateTime <= endDate).ToListAsync();
            var input = new FetchBloodSampleResultRequest(null, null, 0, false, null, bloodSampleInventories.Select(x => x.RegistrationId).Distinct().ToList(), DateTime.MinValue, DateTime.Now, false, null, false);
            var inventoryInput = new FetchBloodBankInventoriesRequest(null, bloodSampleInventories.Select(x => x.InventoryId).Distinct().ToList(), startDate, endDate, DateTime.MinValue, "");
            var registrations = await this.registrationService.GetBloodBankRegistrationsForResult(input);
            var inventories = await this.inventoryService.GetBloodBankInventories(inventoryInput);
            var response = new Dictionary<string, List<string>>();
            var filePrefix = FrameFilePrefix();
            var currentIndex = 1;
            if (!registrations.IsError && !inventories.IsError)
            {
                var patients = registrations.Value.Select(x => x.BloodBankPatient.Identifier).Distinct().ToList();
                patients.ForEach(patient =>
                {
                    var patientRegistrations = registrations.Value.Where(x => x.BloodBankPatient.Identifier == patient).ToList();
                    var registrationBloodSampleInventories = bloodSampleInventories.Where(x => patientRegistrations.Any(y => y.RegistrationId == x.RegistrationId)).ToList();
                    var patientInventories = inventories.Value.Where(x => registrationBloodSampleInventories.Any(y => y.InventoryId == x.Identifier)).ToList();

                    patientInventories.ForEach(inventoryData =>
                    {
                        var bloodSampleInventory = registrationBloodSampleInventories.Find(x => x.InventoryId == inventoryData.Identifier);
                        var transfusionComments = bloodSampleInventory?.TransfusionComments ?? "";
                        var transfusionDateTime = bloodSampleInventory?.TransfusionDateTime ?? bloodSampleInventory?.LastModifiedDateTime ?? DateTime.Now;
                        var registration = patientRegistrations.First();
                        var patientHeader = FramePatientHeader(registration, transfusionDateTime);
                        var transfusedInventory = FrameTransfusedDetail(inventoryData, transfusionComments, "1"); //bloodSampleInventory?.TransfusionVolume
                        var content = new List<string>
                        {
                            patientHeader,
                            transfusedInventory
                        };
                        response.TryAdd(filePrefix + currentIndex.ToString().PadLeft(3, '0'), content);
                        currentIndex = currentIndex + 1;
                    });

                });

            }


            return response;
        }

        private string FrameFilePrefix()
        {
            DateTime now = DateTime.Now;
            return GlobalConstants.HospitalFirstLetterCode + (now.Year % 10) + MonthMap[now.Month] + now.ToString("dd");
        }

        private string FrameTransfusedDetail(BloodBankInventory inventory, string transfusionComments, string transfusedVolume)
        {
            var inventoryDetail = "";
            var productCode = GlobalConstants.Products.Find(product => product.Identifier == inventory.ProductCode)?.Code;
            productCode = productCode ?? "";
            _logger.LogInformation("ProductCode {ProductCode}", productCode);
            var bloodGroup = GlobalConstants.BloodGroupMappings.ContainsKey(inventory.AboOnLabel) ? GlobalConstants.BloodGroupMappings[inventory.AboOnLabel] : "   ";
            inventoryDetail += "P";
            inventoryDetail += GlobalConstants.CTMCode;
            inventoryDetail += productCode.Substring(0, productCode.Length > 8 ? 8 : productCode.Length).PadRight(8); 
            if (!string.IsNullOrEmpty(inventory.CalculatedDonationId))
                inventoryDetail += inventory.CalculatedDonationId.Substring(0, inventory.CalculatedDonationId.Length > 16 ? 16 : inventory.CalculatedDonationId.Length).PadRight(16); 
            else
                inventoryDetail += inventory.DonationId.Substring(0, inventory.DonationId.Length > 16 ? 16 : inventory.DonationId.Length).PadRight(16); 
            inventoryDetail += bloodGroup;
            inventoryDetail += transfusionComments.Substring(0, transfusionComments.Length > 50 ? 50 : transfusionComments.Length).PadRight(50); 
            inventoryDetail += transfusedVolume.Substring(0, transfusedVolume.Length > 5 ? 5 : transfusedVolume.Length).PadLeft(5, '0'); 
            return inventoryDetail;
        }

        private static string FramePatientHeader(Models.BloodBankRegistration registration, DateTime transfusionDate)
        {
            Models.BloodBankPatient patient = registration.BloodBankPatient;
            var patientHeader = "";
            patientHeader += "E"; 
            patientHeader += "       "; 
            patientHeader += patient.NRICNumber.Substring(0, patient.NRICNumber.Length > 20 ? 20 : patient.NRICNumber.Length).PadRight(20); 
            patientHeader += registration.LabAccessionNumber.Substring(0, registration.LabAccessionNumber.Length > 15 ? 15 : registration.LabAccessionNumber.Length).PadRight(15); 
            patientHeader += "               "; 
            patientHeader += "                              "; 
            patientHeader += patient.PatientName.Substring(0, patient.PatientName.Length > 30 ? 30 : patient.PatientName.Length).PadRight(30); 
            patientHeader += GlobalConstants.Lookups.Find(x => x.Identifier == patient.GenderId)?.Code.Substring(0, 1); 
            patientHeader += patient.PatientDOB.ToString("yyyyMMdd");
            patientHeader += "                              "; 
            patientHeader += GlobalConstants.BloodGroupMappings.ContainsKey(patient.BloodGroup) ? GlobalConstants.BloodGroupMappings[patient.BloodGroup] : "   "; 
            patientHeader += transfusionDate.ToString("yyyyMMdd");
            patientHeader += transfusionDate.ToString("HH:mm:ss");
            patientHeader += GlobalConstants.FacilityCode;
            patientHeader += GlobalConstants.Lookups.Find(x => x.Identifier == patient.ResidenceStatusId)?.Code; 
            patientHeader += GlobalConstants.DepotCode;
            return patientHeader;
        }
    }
}