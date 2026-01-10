using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpdateStandardPatientRequest
    (
        Int64 RegistrationID,
        string? NRICNo,
        string? PatientName,
        DateTime? PatientDOB,        
        Int64? GenderId,
        Int64? UserNo,
        DateTime? LastModifiedDateTime,
        int? VenueNo,
        int? VenueBranchNo,
         Int64? NationalityId,
        Int64? RaceId,
        Int64? ResidenceStatusId,
        string? Result,
        string? Comments
    );
   
}