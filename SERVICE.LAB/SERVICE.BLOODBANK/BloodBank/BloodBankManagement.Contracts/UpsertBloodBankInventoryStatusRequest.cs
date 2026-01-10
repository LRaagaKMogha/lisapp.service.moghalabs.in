using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpsertBloodBankInventoryStatusRequest
    (
        Int64 InventoryId,
        string Status,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        string Comments
    );
    
}