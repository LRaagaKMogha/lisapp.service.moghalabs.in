using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QC.Validators;
using QCManagement.Contracts;
using Shared;

namespace QCManagement.Models
{
    public class ControlMaster
    {
        public Int64 Identifier { get; private set; }
        public string ControlName { get; set; }
        public string ControlType { get; set; } //Assayed, Unassayed
        public string LotNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Int64 ManufacturerID { get; set; }
        public Int64 DistributorID { get; set; }
        public string Notes { get; set; }
        public string Form { get; set; } //lypholized, liquid, liquid frozen form
        public Int64 Level { get; set; }
        public Int64 ReagentID { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public ICollection<TestControlSamples> TestControlSamples { get; set; } = new List<TestControlSamples>();
        public string Status { get; set; } = "Captured";
        public DateTime? PreparationDateTime { get; set; }
        public Int64? PreparedBy { get; set; }
        public string? PreparedByUserName { get; set; }
        public Int64? StorageTemperature { get; set; }
        public Int64? AliquoteCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsQualitative { get; set;}     
        public bool IsAntibiotic { get; set; }
        public ControlMaster()
        {

        }
        public ControlMaster(Int64 identifier, string controlName, string controlType, string lotNumber, DateTime expirationDate, Int64 manufacturerID, Int64 distributorID, string notes, string form, Int64 level, Int64 reagentID, List<TestControlSamples> testcontrolSamples, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool isActive, string status, bool isQualitative, bool isAntibiotic)
        {
            Identifier = identifier;
            ControlName = controlName;
            ControlType = controlType;
            LotNumber = lotNumber;
            ExpirationDate = expirationDate;
            ManufacturerID = manufacturerID;
            DistributorID = distributorID;
            Notes = notes;
            Form = form;
            Level = level;
            ReagentID = reagentID;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            TestControlSamples = testcontrolSamples;
            IsActive = isActive;
            Status = status;
            IsQualitative = isQualitative;
            IsAntibiotic = isAntibiotic;
        }

        public static ErrorOr<ControlMaster> From(ControlMasterRequest request, HttpContext httpContext)
        {
            var tests = request.TestControlSamples.Select(x => new TestControlSamples(x.Identifier, x.TestID, x.SubTestID, x.QcMonitoringMethod, x.ControlLimit, x.ParameterName, x.ControlID, x.TargetRangeMin, x.TargetRangeMax, x.SampleTypeId, x.UoMId, x.UomText, x.UoMId2, x.UomText2, x.Mean, x.Cv, x.SD, x.ModifiedBy, x.ModifiedByUserName, x.LastModifiedDateTime, x.IsSelected, x.StartTime, x.EndTime, x.DecimalPlaces)).ToList();
            var response = new ControlMaster(
                request.Identifier,
                request.ControlName,
                request.ControlType,
                request.LotNumber,
                request.ExpirationDate,
                request.ManufacturerID,
                request.DistributorID,
                request.Notes,
                request.Form,
                request.Level,
                request.ReagentID,
                tests,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.IsActive,
                request.Status,
                request.IsQualitative,
                request.IsAntibiotic
            );
            var errors = Helpers.ValidateInput<ControlMaster, ControlMasterValidator>(response, httpContext);
            if (errors.Count > 0)
                return errors;
            return response;

        }
    }
}