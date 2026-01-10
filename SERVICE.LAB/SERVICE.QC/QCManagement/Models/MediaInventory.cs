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
    public class MediaInventory
    {
        public Int64 Identifier { get; set; }
        public Int64 BatchId { get; set; }  
        public DateTime ReceivedDateAndTime { get; set; }
        public Int64 MediaId { get; set; }
        public string MediaLotNumber { get; set; }
        public DateTime ExpirationDateAndTime { get; set; }
        public string Colour { get; set; } // Normal or Abnormal
        public string Crack { get; set; } // Yes or No
        public string Contaminate { get; set; }
        public string Leakage { get; set; }
        public string Turbid { get; set; }
        public string VolumeOrAgarThickness { get; set; }
        public string Sterlity { get; set; }
        public string Vividity { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public MediaInventory()
        {
        }

        public MediaInventory(Int64 identifier, DateTime receivedDateAndTime, Int64 mediaId, string mediaLotNumber, DateTime expirationDateAndTime, string colour, string crack, string contaminate, string leakage, string turbid, string volumeOrAgarThickness, string sterlity, string vividity, string remarks, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, bool isActive, Int64 batchId)
        {
            Identifier = identifier;
            ReceivedDateAndTime = receivedDateAndTime;
            MediaId = mediaId;
            MediaLotNumber = mediaLotNumber;
            ExpirationDateAndTime = expirationDateAndTime;
            Colour = colour;
            Crack = crack;
            Contaminate = contaminate;
            Leakage = leakage;
            Turbid = turbid;
            VolumeOrAgarThickness = volumeOrAgarThickness;
            Sterlity = sterlity;
            Vividity = vividity;
            Remarks = remarks;
            Status = status;
            ModifiedBy = modifiedBy;
            ModifiedByUserName = modifiedByUserName;
            LastModifiedDateTime = lastModifiedDateTime;
            IsActive = isActive;
            BatchId = batchId;  
        }

        public static ErrorOr<MediaInventory> From(MediaInventoryRequest request, HttpContext httpContext)
        {
            var input = new MediaInventory(
                request.Identifier,
                request.ReceivedDateAndTime,
                request.MediaId,
                request.MediaLotNumber,
                request.ExpirationDateAndTime,
                request.Colour,
                request.Crack,
                request.Contaminate,
                request.Leakage,
                request.Turbid,
                request.VolumeOrAgarThickness,
                request.Sterlity,
                request.Vividity,
                request.Remarks,
                request.Status,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.IsActive,
                request.BatchId);

            var errors = Helpers.ValidateInput<MediaInventory, MediaInventoryValidator>(input, httpContext);
            if (errors.Count > 0)
                return errors;
            return input;
        }
    }

}