using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;
using QCManagement.Validators;
using Shared;

namespace QCManagement.Models
{
    public class QCResultEntry
    {
        public Int64 Identifier { get; private set; }
        public Int64 TestID { get;  set; }
        public string ParameterName { get; set; }
        public Int64 ControlID { get;  set; }
        public Int64 AnalyzerID { get;  set; }
        public string QcMonitoringMethod { get; set; }
        public bool IsMicroQc { get; set; }
        public DateTime TestDate { get;  set; }
        public decimal ObservedValue { get;  set; }
        public string Comments { get;  set; }
        public string Status { get;  set; } = "OK";
        public string? BatchId { get; set; } = "0";
        public bool IsActive { get; set; }
        public Int64 ModifiedBy { get;  set; }
        public string ModifiedByUserName { get;  set; }
        public DateTime LastModifiedDateTime { get;  set; }
        public Int64 TestControlSamplesID { get; set; } 
        public bool IsAntibiotic { get; set; }
        public QCResultEntry(Int64 identifier, Int64 testID, string parameterName, Int64 controlID, Int64 analyzerID, string qcMonitoringMethod, DateTime testDate, decimal observedValue,  string comments, string? batchId, string? status, bool isActive, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, Int64 testControlSamplesID, bool isMicroQc, bool isAntibiotic)
        {
            Identifier = identifier;
            TestID = testID;
            ParameterName = parameterName;
            ControlID = controlID;
            AnalyzerID = analyzerID;
            QcMonitoringMethod = qcMonitoringMethod;
            TestDate = testDate;
            ObservedValue = observedValue;
            Comments = comments;
            BatchId = batchId;
            Status = status ?? "OK";
            IsActive = isActive;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            TestControlSamplesID = testControlSamplesID;
            IsMicroQc = isMicroQc;
            IsAntibiotic = isAntibiotic;
        }

        public static ErrorOr<QCResultEntry> From(QCResultEntryRequest request, HttpContext httpContext)
        {
            var response = new QCResultEntry(
                request.Identifier,
                request.TestID,
                request.ParameterName,
                request.ControlID,
                request.AnalyzerID,
                request.QcMonitoringMethod,
                request.TestDate,
                request.ObservedValue,
                request.Comments,
                request.BatchId,
                request.Status,
                request.IsActive,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.TestControlSamplesID,
                request.IsMicroQc,
                request.IsAntibiotic
            );
            var errors = Helpers.ValidateInput<QCResultEntry, QCResultEntryValidator>(response, httpContext);
            if (errors.Count > 0)
                return errors;
            return response;
        }
    }

}