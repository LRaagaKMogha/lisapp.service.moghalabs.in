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
    public class MicroQCMaster
    {
        public Int64 Identifier { get; set; }
        public Int64 CultureReagentTestId { get; set; }
        public string PositiveStrainIds { get; set; }
        public string NegativeStrainIds { get; set; }
        public string Frequency { get; set; }
        public string Status { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public MicroQCMaster()
        {
        }

        public MicroQCMaster(Int64 identifier, Int64 cultureReagentTestId, string positiveStrainIds, string negativeStrainIds,  string frequency, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool isActive)
        {
            Identifier = identifier;
            CultureReagentTestId = cultureReagentTestId;
            PositiveStrainIds = positiveStrainIds;
            NegativeStrainIds = negativeStrainIds;
            Frequency = frequency;
            Status = status;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            IsActive = isActive;
        }

        public static ErrorOr<MicroQCMaster> From(MicroQCMasterRequest request, HttpContext httpContext)
        {
            var input = new MicroQCMaster(
                request.Identifier,
                request.CultureReagentTestId,
                request.PositiveStrainIds,
                request.NegativeStrainIds,
                request.Frequency,
                request.Status,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.IsActive);

            var errors = Helpers.ValidateInput<MicroQCMaster, MicroQCMasterValidator>(input, httpContext);
            if (errors.Count > 0)
                return errors;
            return input;
        }
    }
}
