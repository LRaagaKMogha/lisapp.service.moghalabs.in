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
    public class StrainMaster
    {
        public Int64 Identifier { get; set; }
        public Int64 StrainCategoryId { get; set; }
        public string StrainName { get; set; }
        public Int64 ExpiryAlertPeriod { get; set; }
        public string LinkedMedias { get; set; }
        public string Status { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public StrainMaster() { }

        public StrainMaster(Int64 identifier, Int64 strainCategoryId, string strainName, Int64 expiryAlertPeriod, string linkedMedias, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool isActive)
        {
            Identifier = identifier;
            StrainCategoryId = strainCategoryId;
            StrainName = strainName;
            ExpiryAlertPeriod = expiryAlertPeriod;
            LinkedMedias = linkedMedias;
            Status = status;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            IsActive = isActive;
        }

        public static ErrorOr<StrainMaster> From(StrainMasterRequest request, HttpContext httpContext)
        {
            var input = new StrainMaster(
                request.Identifier,
                request.StrainCategoryId,
                request.StrainName,
                request.ExpiryAlertPeriod,
                request.LinkedMedias,
                request.Status,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.IsActive);

            var errors = Helpers.ValidateInput<StrainMaster, StrainMasterValidator>(input, httpContext);
            if (errors.Count > 0)
                return errors;
            return input;
        }
    }
}
