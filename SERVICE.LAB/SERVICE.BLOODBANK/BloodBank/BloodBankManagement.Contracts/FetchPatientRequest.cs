using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchPatientRequest
    (
        Int64 RegistrationID,
        int? MasterNo,
        int? VenueNo,
        int? VenueBranchNo,
        int? PageIndex,
        string? NRICNo,
        string? FromDate,
        string? ToDate,
        string? Type,
        int? PatientVisitNo,
        bool? eLab,
        int? WardNo,
        string? CaseNo,
        int? CustomerNo,
        int? PhysicianNo,
        int? TestNo
    );   
}