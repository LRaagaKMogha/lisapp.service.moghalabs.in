using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QCManagement.Contracts;

namespace QCManagement.Models
{
    public class TestControlSamples
    {
        public Int64 Identifier { get; private set; }
        public Int64? TestID { get;  set; }
        public Int64? SubTestID { get; set; }
        public string QcMonitoringMethod { get; set; }
        public string ControlLimit { get; set; }
        public string? ParameterName { get; set; }
        public Int64 ControlID { get;  set; }
        public decimal TargetRangeMin { get;  set; }
        public decimal TargetRangeMax { get;  set; }
        public Int64 SampleTypeId { get;  set; }
        public Int64 UoMId { get;  set; }       
        public string UomText  { get; set;}
        public Int64 UoMId2 { get; set; }
        public string UomText2 { get; set; }
        public Int32 DecimalPlaces { get; set; }    
        public decimal Mean { get;  set; }
        public decimal SD { get;  set; }
        public decimal Cv { get; set;}
        public Int64 ModifiedBy { get;  set; }
        public string ModifiedByUserName { get;  set; }
        public DateTime LastModifiedDateTime { get;  set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsSelected { get; set; }
        // Navigation properties
        public ControlMaster ControlMaster { get; set; }
        // public ICollection<QCResultEntry> QCResultEntries { get; set; } = new List<QCResultEntry>();

        public TestControlSamples()
        {
            
        }

        public TestControlSamples(Int64 id, Int64? testID, Int64? subTestID, string qcMonitoringMethod, string controlLimit, string? parameterName,  Int64 controlID, decimal targetRangeMin, decimal targetRangeMax, Int64 sampleTYpeId,  Int64 uomId, string uomText, Int64 uomId2, string uomText2, decimal mean, decimal cv, decimal sd, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool isSelected, DateTime? startTime, DateTime? endTime, Int32 decimalPlaces)
        {
            Identifier = id;
            TestID = testID;
            SubTestID = subTestID;
            QcMonitoringMethod = qcMonitoringMethod;
            ControlLimit = controlLimit;
            ParameterName = parameterName;
            ControlID = controlID;
            TargetRangeMin = targetRangeMin;
            TargetRangeMax = targetRangeMax;
            SampleTypeId = sampleTYpeId;
            UoMId = uomId;
            UomText = uomText;
            UoMId2 = uomId2;
            UomText2 = uomText2;
            Mean = mean;
            Cv = cv;
            SD = sd;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            IsSelected = isSelected;
            StartTime = startTime;
            EndTime = endTime;
            DecimalPlaces = decimalPlaces;
        }

        public static TestControlSamples From(TestControlSamplesRequest request, HttpContext httpContext)
        {
            return new TestControlSamples(
                request.Identifier,
                request.TestID,
                request.SubTestID, 
                request.QcMonitoringMethod,
                request.ControlLimit,
                request.ParameterName,
                request.ControlID,
                request.TargetRangeMin,
                request.TargetRangeMax,
                request.SampleTypeId,
                request.UoMId,
                request.UomText,
                request.UoMId2,
                request.UomText2,
                request.Mean, 
                request.Cv,
                request.SD,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.IsSelected,
                request.StartTime, 
                request.EndTime,
                request.DecimalPlaces
            );
        }
    }
}