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
    public class StrainInventory
    {
        public Int64 Identifier { get; set; }
        public Int64 BatchId { get; set; }
        public DateTime ReceivedDateAndTime { get; set; }
        public Int64 StrainId { get; set; }
        public string StrainLotNumber { get; set; }
        public DateTime ExpirationDateAndTime { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public StrainInventory()
        {
        }

        public StrainInventory(Int64 identifier, Int64 batchId, DateTime receivedDateAndTime, Int64 strainId, string strainLotNumber, DateTime expirationDateAndTime, string remarks, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool isActive)
        {
            Identifier = identifier;
            BatchId = batchId;
            ReceivedDateAndTime = receivedDateAndTime;
            StrainId = strainId;
            StrainLotNumber = strainLotNumber;
            ExpirationDateAndTime = expirationDateAndTime;
            Remarks = remarks;
            Status = status;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            IsActive = isActive;
        }

        public static ErrorOr<StrainInventory> From(StrainInventoryRequest request, HttpContext httpContext)
        {
            var input = new StrainInventory(
                request.Identifier,
                request.BatchId,
                request.ReceivedDateAndTime,
                request.StrainId,
                request.StrainLotNumber,
                request.ExpirationDateAndTime,
                request.Remarks,
                request.Status,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.IsActive);

            var errors = Helpers.ValidateInput<StrainInventory, StrainInventoryValidator>(input, httpContext);
            if (errors.Count > 0)
                return errors;
            return input;
        }
    }
}
