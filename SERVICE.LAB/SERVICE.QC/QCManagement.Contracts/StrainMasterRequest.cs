using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record StrainMasterResponse(
        long Identifier,
        long StrainCategoryId,
        string StrainName,
        long ExpiryAlertPeriod,
        string LinkedMedias,
        string Status,
        long ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool IsActive
    );
}