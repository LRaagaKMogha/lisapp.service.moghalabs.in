using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record UpsertStrainInventoriesRequest(
        List<StrainInventoryRequest> Inventories
    );
    public record StrainInventoryRequest(
        long Identifier,
        long BatchId,
        DateTime ReceivedDateAndTime,
        long StrainId,
        string StrainLotNumber,
        DateTime ExpirationDateAndTime,
        string Remarks,
        string Status,
        long ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool IsActive
    );
}