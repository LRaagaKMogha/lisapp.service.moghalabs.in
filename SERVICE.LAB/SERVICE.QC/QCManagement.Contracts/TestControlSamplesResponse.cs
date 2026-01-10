using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record TestControlSamplesResponse(
        Int64 TestControlSamplesID,
        Int64? TestID,
        Int64? SubTestID,
        string QcMonitoringMethod,
        string ControlLimit,
        string? ParameterName,
        Int64 ControlID,
        decimal TargetRangeMin,
        decimal TargetRangeMax,
        Int64 SampleTypeId,
        Int64 UoMId,
        string UomText,
        Int64 UoMId2,
        string UomText2,
        decimal Mean,
        decimal Cv,
        decimal SD,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool IsSelected,
        DateTime? StartTime, 
        DateTime? EndTime     ,
        Int32 DecimalPlaces
    );
}