using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record StrainMasterFilterRequest(
        DateTime? StartDate,
        DateTime? EndDate,
        bool showAllActive,
        string? StrainName, 
        Int64? StrainCategoryId,
        string? ActiveStatus
    );
}
