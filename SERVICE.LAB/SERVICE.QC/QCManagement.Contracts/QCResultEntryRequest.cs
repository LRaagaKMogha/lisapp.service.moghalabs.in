using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record UpsertQCResultEntryRequest
    (
        string Type,
        List<QCResultEntryRequest> QCResultEntries
    );
    public record QCResultEntryRequest(
        Int64 Identifier,
        Int64 TestID,
        string ParameterName,
        Int64 ControlID,
        Int64 AnalyzerID,
        string QcMonitoringMethod,
        DateTime TestDate,
        decimal ObservedValue,
        string Comments,
        string? BatchId,
        string? Status,
        bool IsActive,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        Int64 TestControlSamplesID, 
        bool IsMicroQc, 
        bool IsAntibiotic
    );
}