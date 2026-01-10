using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record QCResultEntryFilterRequest
     (
        Int64 AnalyzerID,
        Int64 ControlID,
        List<Int64>? ControlIDs,
        Int64 TestID,
        DateTime? StartDate,
        DateTime? EndDate,
        DateTime? ExpirationDate,
        string? QcMonitoringMethod,
        bool showAllActive,
        bool IsMicroQc,
        bool IsAntibiotic,
        string? ControlName,
        string? ActiveStatus,
        Int64 TestControlSamplesID,
        bool IsFilterBasedOnTestDate
    );
}