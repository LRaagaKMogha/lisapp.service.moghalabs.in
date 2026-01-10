using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Models
{
    public class Scheduler
    {
        public Int64 Identifier { get; private set; }
        public Int64 AnalyzerId { get; private set; }
        public Int64 TestId { get; private set; }
        public string ScheduleTitle { get; private set; }
        public DateTime ScheduleStartDate { get;  set; }
        public DateTime ScheduleEndDate { get;  set; }
        public Int64 BatchId { get; set; }
        public Int64 ModifiedBy { get; private set; }
        public string ModifiedByUserName { get; private set; }
        public DateTime LastModifiedDateTime { get; private set; }

        public Scheduler(Int64 identifier, Int64 analyzerId, Int64 testId, string scheduleTitle, DateTime scheduleStartDate, DateTime scheduleEndDate, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime)
        {
            Identifier = identifier;
            AnalyzerId = analyzerId;
            TestId = testId;
            ScheduleTitle = scheduleTitle;
            ScheduleStartDate = scheduleStartDate;
            ScheduleEndDate = scheduleEndDate;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;

        }

        public static ErrorOr<Scheduler> From(SchedulerRequest request, HttpContext httpContext)
        {
              return new Scheduler(
                request.Identifier,
                request.AnalyzerId,
                request.TestId,
                request.ScheduleTitle,
                request.ScheduleStartDate,
                request.ScheduleEndDate,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime
            );
        }
    }
}