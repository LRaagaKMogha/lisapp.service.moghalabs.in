using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record UpsertStrainMasterStatusRequest(
        long StrainMasterId,
        string Status,
        long ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        string Comments
    );
}
