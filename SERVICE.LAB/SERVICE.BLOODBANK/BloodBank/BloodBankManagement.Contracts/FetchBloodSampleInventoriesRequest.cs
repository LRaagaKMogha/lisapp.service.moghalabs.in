using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchBloodSampleInventoriesRequest
    (
        List<Int64> RegistrationIds, 
        Int64 BloodSampleId
    );
}
