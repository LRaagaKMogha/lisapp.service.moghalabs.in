using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record MicroQCMasterFilterRequest(
        DateTime? StartDate,
        DateTime? EndDate,
        bool showAllActive,
        Int64 reagentId,
        string? ActiveStatus
    );
}