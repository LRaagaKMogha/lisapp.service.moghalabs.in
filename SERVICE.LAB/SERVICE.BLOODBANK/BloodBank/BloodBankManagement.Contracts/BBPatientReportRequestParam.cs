using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record BBPatientReportRequestParam
     (
         int RowNo,
         int PatientNo,
         int RegistrationNo,
         int IdentityNo,
         int VenueNo,
         int VenueBranchNo,
         int UserNo,
         string TestNos,
         int TestNo,
         bool IsLogo,
         string PageCode,
         string ReportType,
         string ReportKey
     );
}