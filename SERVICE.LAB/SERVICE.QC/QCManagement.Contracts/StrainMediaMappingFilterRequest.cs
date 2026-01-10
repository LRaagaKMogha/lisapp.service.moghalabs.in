using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record StrainMediaMappingFilterRequest(
        DateTime? StartDate,
        DateTime? EndDate,
        bool showAllActive,
        Int64? StrainInventoryId,
        Int64? MediaInventoryId,
        string? ActiveStatus,
        string? MappingStatus
    );
}