using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Validators;
using ErrorOr;
using Shared;

namespace BloodBankManagement.Models
{
    public class BloodBankRegistration
    {
        public Int64 RegistrationId { get; set; }
        public string NRICNumber { get; set; } = "";
        public string PatientName { get; set; }
        public DateTime PatientDOB { get; set; }
        public string CaseOrVisitNumber { get; set; }
        public Int64 NationalityId { get; set; }
        public Int64 GenderId { get; set; }
        public Int64 RaceId { get; set; }
        public Int64 ResidenceStatusId { get; set; }
        public Int64? ClinicalDiagnosisId { get; set; }
        public Int64 IndicationOfTransfusionId { get; set; }
        public string ClinicalDiagnosisOthers { get; set; }
        public string IndicationOfTransfusionOthers { get; set; }
        public string DoctorOthers { get; set; }
        public string DoctorMCROthers { get; set; }

        public bool IsEmergency { get; set; }
        public Int64? WardId { get; set; }
        public Int64? ClinicId { get; set; }
        public Int64? DoctorId { get; set; }
        public ICollection<RegisteredProduct> Products { get; set; } = new List<RegisteredProduct>();
        public ICollection<RegistrationTransaction> Transactions { get; } = new List<RegistrationTransaction>();
        public ICollection<BloodBankBilling> Billings { get; set; } = new List<BloodBankBilling>();


        public ICollection<RegisteredSpecialRequirement> SpecialRequirements { get; set; } = new List<RegisteredSpecialRequirement>();
        public ICollection<BloodSampleResult> Results { get; set; } = new List<BloodSampleResult>();
        public ICollection<BloodSampleInventory> BloodSampleInventories { get; set; } = new List<BloodSampleInventory>();
        public decimal ProductTotal { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public BloodBankPatient BloodBankPatient { get; set; }
        public Int64 BloodBankPatientId { get; set; }
        public Int64? NurseId { get; set; }
        public string IssuingComments { get; set; }
        public string LabAccessionNumber { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime? SampleReceivedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public DateTime RegistrationDateTime { get; set; } = DateTime.Now;
        public Int64 PatientVisitNo { get; set; }
        public string VisitId {  get; set; }
        public BloodBankRegistration()
        {

        }

        public BloodBankRegistration(
            Int64 identifier,
            string nricNumber,
            string patientName,
            DateTime patientDOB,
            string caseOrVisitNumber,
            Int64 nationalityId,
            Int64 genderId,
            Int64 raceId,
            Int64 residenceStatusId,
            Int64? clinicalDiagnosisId,
            Int64 indicationOfTransfusionId,
            string clinicalDiagnosisOthers,
            string indicationOfTransfusionOthers,
            string doctorOthers,
            string doctorMCROthers,
            bool isEmergency,
            Int64? wardId,
            Int64? clinicId,
            Int64? doctorId,
            List<RegisteredProduct> products,
            List<RegisteredSpecialRequirement> specialRequirements,
            List<BloodSampleResult> results,
            decimal productTotal,
            bool isActive,
            string status,
            Int64? nurseId,
            string issuingComments,
            string labAccessionNumber,
            Int64 modifiedBy,
            string modifiedByUserName,
            DateTime lastModifiedDateTime
        )
        {
            RegistrationId = identifier;
            NRICNumber = nricNumber;
            PatientName = patientName;
            PatientDOB = patientDOB;
            CaseOrVisitNumber = caseOrVisitNumber;
            NationalityId = nationalityId;
            GenderId = genderId;
            RaceId = raceId;
            ResidenceStatusId = residenceStatusId;
            ClinicalDiagnosisId = clinicalDiagnosisId;
            IndicationOfTransfusionId = indicationOfTransfusionId;
            ClinicalDiagnosisOthers = clinicalDiagnosisOthers;
            IndicationOfTransfusionOthers = indicationOfTransfusionOthers;
            DoctorOthers = doctorOthers;
            DoctorMCROthers = doctorMCROthers;
            IsEmergency = isEmergency;
            WardId = wardId;
            ClinicId = clinicId;
            DoctorId = doctorId;
            Products = products;
            SpecialRequirements = specialRequirements;
            Results = results;
            ProductTotal = productTotal;
            IsActive = isActive;
            Status = status;
            NurseId = nurseId;
            IssuingComments = issuingComments;
            LabAccessionNumber = labAccessionNumber;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            RegistrationDateTime = lastModifiedDateTime;
        }

        public static ErrorOr<BloodBankRegistration> Create(
            string nricNumber,
            string patientName,
            DateTime patientDOB,
            string caseOrVisitNumber,
            Int64 nationalityId,
            Int64 genderId,
            Int64 raceId,
            Int64 residenceStatusId,
            Int64? clinicalDiagnosisId,
            Int64 indicationOfTransfusionId,
            string clinicalDiagnosisOthers,
            string indicationOfTransfusionOthers,
            string doctorOthers,
            string doctorMCROthers,            
            bool isEmergency,
            List<RegisteredProduct> products,
            List<RegisteredSpecialRequirement> specialRequirements,
            List<BloodSampleResult> results,
            decimal productTotal,
            bool isActive,
            string status,
            Int64? nurseId,
            string issuingComments,
            string labAccessionNumber,
            Int64 modifiedBy,
            string modifiedByUserName,
            DateTime lastModifiedDateTime,
            Int64? wardId,
            Int64? clinicId,
            Int64? doctorId,
            Int64? identifier = null)
        {
            return new BloodBankRegistration(identifier ?? 0, nricNumber, patientName, patientDOB, caseOrVisitNumber, nationalityId, genderId, raceId, residenceStatusId, clinicalDiagnosisId, indicationOfTransfusionId, clinicalDiagnosisOthers, indicationOfTransfusionOthers, doctorOthers, doctorMCROthers, isEmergency, wardId, clinicId, doctorId, products, specialRequirements, results, productTotal, isActive, status, nurseId, issuingComments, labAccessionNumber, modifiedBy, modifiedByUserName, lastModifiedDateTime);
        }

        public static ErrorOr<BloodBankRegistration> From(UpsertBloodBankRegistrationRequest request, HttpContext httpContext)
        {
            var products = request.Products.Select(x => new RegisteredProduct(x.ProductId, x.MRP, x.Unit, x.Price)).ToList();
            var results = request.Results.Select(x => new BloodSampleResult(x.Identifier, x.RegistrationId, x.ContainerId, x.TestId, x.ParentTestId, x.InventoryId, x.TestName, x.Unit, x.TestValue, x.Comments, x.BarCode, x.Status, x.IsRejected, x.ModifiedBy, x.ModifiedByUserName, x.LastModifiedDateTime, false, x.interfaceispicked, 0, x.GroupId, x.ParentRegistrationId)).ToList();
            var specialRequirements = request.SpecialRequirements.Select(x => new RegisteredSpecialRequirement(x.Identifier, x.RegistrationId, x.SpecialRequirementId, x.Validity, x.ModifiedBy, x.ModifiedByUserName, 0)).ToList();
            var response = Create(request.NRICNumber, request.PatientName, request.PatientDOB, request.CaseOrVisitNumber, request.NationalityId, request.GenderId, request.RaceId, request.ResidenceStatusId, request.ClinicalDiagnosisId, request.IndicationOfTransfusionId,
            request.ClinicalDiagnosisOthers, request.IndicationOfTransfusionOthers, request.DoctorOthers, request.DoctorMCROthers, request.IsEmergency
            , products, specialRequirements, results, request.ProductTotal, request.IsActive, request.Status, request.NurseId, request.IssuingComments, request.LabAccessionNumber, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.WardId, request.ClinicId, request.DoctorId, request.Identifier);
             List<Error> errors = Shared.Helpers.ValidateInput<BloodBankRegistration, BloodBankRegistrationValidator>(response.Value, httpContext);
            if (errors.Count > 0)
            {
                return errors;
            }
            return response;
        }

        
    }

}
