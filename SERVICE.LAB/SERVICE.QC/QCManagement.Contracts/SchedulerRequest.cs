using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record SchedulerRequest
    (
        Int64 Identifier,
        Int64 AnalyzerId,
        Int64 TestId,
        string ScheduleTitle,
        DateTime ScheduleStartDate,
        DateTime ScheduleEndDate,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime
    );

    public record SchedulerRequests
    (
        Int64 Identifier,
        Int64 AnalyzerId,
        Int64 TestId,
        string ScheduleTitle,
        DateTime ScheduleStartDate,
        DateTime ScheduleEndDate,
        string[] Times,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime
    );
}