using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodBankRegistration(
        Int64 Identifier,
        string NRICNumber,
        string PatientName,
        DateTime PatientDOB,
        string CaseOrVisitNumber,
        Int64 NationalityId,
        Int64 GenderId,
        Int64 RaceId,
        Int64 ResidenceStatusId,
        Int64? ClinicalDiagnosisId,
        Int64 IndicationOfTransfusionId,
        string ClinicalDiagnosisOthers,
        string IndicationOfTransfusionOthers,
        string DoctorOthers,
        string DoctorMCROthers,
        bool IsEmergency,
        Int64? WardId,
        Int64? ClinicId,
        Int64? DoctorId,
        List<PatientRegisteredProducts> Products,
        List<RegisteredSpecialRequirementResponse> SpecialRequirements,
        List<BloodSampleResultResponse> Results,
        List<RegistrationTransactionResponse> Transactions,
        BloodBankPatientResponse Patient,
        decimal ProductTotal,
        bool IsActive,
        string Status,
        Int64? NurseId,
        string IssuingComments,
        string LabAccessionNumber,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        DateTime? SampleReceivedDateTime,
        DateTime? RegistrationDateTime,
        List<BloodSampleInventoryResponse> BloodSampleInventories,
        List<BloodBankBillingResponse> Billings,
        Int64? PatientVisitNo,
        string? VisitId
    );
}
