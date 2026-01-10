using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record SchedulerResponse
     (
        Int64 Identifier,
        Int64 AnalyzerId,
        Int64 TestId,
        string ScheduleTitle,
        DateTime ScheduleStartDate,
        DateTime ScheduleEndDate,   
        Int64 BatchId, 
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime
    );
}