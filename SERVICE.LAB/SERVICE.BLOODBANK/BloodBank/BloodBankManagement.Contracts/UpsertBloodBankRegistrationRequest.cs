using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record RecallRequest (
        Int64 RegistrationId
        );
    public record UpsertBloodBankRegistrationRequest(
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
        List<BloodSampleResultResponse> Results,
        List<RegisteredSpecialRequirementResponse> SpecialRequirements,
        decimal ProductTotal,
        bool IsActive,
        string Status,
        Int64? NurseId,
        string IssuingComments,
        string LabAccessionNumber,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        Int64? Identifier,
        DateTime LastModifiedDateTime
    );



}
