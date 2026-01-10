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
    public class StrainMediaMapping
    {
        public Int64 Identifier { get; set; }
        public DateTime ReceivedDateAndTime { get; set; }
        public Int64 StrainInventoryId { get; set; }
        public Int64 MediaInventoryId { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public Int64 BatchId { get; set; } = 0;
        public StrainMediaMapping()
        {
        }

        public StrainMediaMapping(Int64 identifier, DateTime receivedDateAndTime, Int64 strainInventoryId, Int64 mediaInventoryId, string remarks, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool isActive)
        {
            Identifier = identifier;
            ReceivedDateAndTime = receivedDateAndTime;
            StrainInventoryId = strainInventoryId;
            MediaInventoryId = mediaInventoryId;
            Remarks = remarks;
            Status = status;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            IsActive = isActive;
        }

        public static ErrorOr<StrainMediaMapping> From(StrainMediaMappingRequest request, HttpContext httpContext)
        {
            var input = new StrainMediaMapping(
                request.Identifier,
                request.ReceivedDateAndTime,
                request.StrainInventoryId,
                request.MediaInventoryId,
                request.Remarks,
                request.Status,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.IsActive);

            var errors = Helpers.ValidateInput<StrainMediaMapping, StrainMediaMappingValidator>(input, httpContext);
            if (errors.Count > 0)
                return errors;
            return input;
        }
    }
}
