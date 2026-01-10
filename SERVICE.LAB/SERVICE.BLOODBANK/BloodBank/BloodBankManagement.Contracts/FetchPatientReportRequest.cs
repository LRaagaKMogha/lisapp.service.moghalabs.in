using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchPatientReportRequest
    (
        Int64 RegistrationID,
        Int64? TestNo,
        int? VenueNo,
        int? VenueBranchNo
    );
   
}