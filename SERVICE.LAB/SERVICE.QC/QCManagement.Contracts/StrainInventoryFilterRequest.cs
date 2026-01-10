using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record StrainInventoryFilterRequest(
        DateTime? StartDate,
        DateTime? EndDate,
        bool showAllActive,
        List<Int64>? StrainMasterIds,
        Int64? StrainId,
        string? ActiveStatus,
        List<Int64>? StrainIds
    );
}