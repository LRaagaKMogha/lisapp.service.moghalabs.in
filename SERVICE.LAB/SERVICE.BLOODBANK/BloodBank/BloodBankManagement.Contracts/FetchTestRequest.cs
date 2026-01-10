using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchTestRequest
    (
        int DeptNo,
        int VenueNo,
        int VenueBranchNo,
        int TestNo
    );
   
}