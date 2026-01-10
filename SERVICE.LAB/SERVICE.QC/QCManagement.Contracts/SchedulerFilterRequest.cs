using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record SchedulerFilterRequest
    (
        DateTime? StartDate,
        DateTime? EndDate,
        DateTime? ExpirationDate,
        Int64? AnalyzerID,
        Int64? TestID
    );
}