using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record UpsertMediaInventoriesRequest(
        List<MediaInventoryRequest> Inventories
    );
    public record MediaInventoryRequest(
        Int64 Identifier,
        Int64 BatchId,
        DateTime ReceivedDateAndTime,
        Int64 MediaId,
        string MediaLotNumber,
        DateTime ExpirationDateAndTime,
        string Colour, // Normal or Abnormal
        string Crack, // Yes or No
        string Contaminate,
        string Leakage,
        string Turbid,
        string VolumeOrAgarThickness,
        string Sterlity,
        string Vividity,
        string Remarks,
        string Status,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool IsActive
    );
}