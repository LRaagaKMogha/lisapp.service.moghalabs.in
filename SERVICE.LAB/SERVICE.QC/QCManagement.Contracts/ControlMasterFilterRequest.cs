using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record ControlMasterFilterRequest
    (
        DateTime? StartDate,
        DateTime? EndDate,
        DateTime? ExpirationDate,
        long? ControlMasterId,
        string? QcMonitoringMethod,
        bool showAllActive,
        bool IsAntibiotic,
        string? controlMasterName,
        string? ActiveStatus,
        long? StrainCategoryId,
        long? AnalyzerID
    );
}