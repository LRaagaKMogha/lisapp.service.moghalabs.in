using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodBankInventoryTransactionResponse
    (
        Int64 Identifier, 
        string InventoryStatus,
        string Comments,
        Int64 ModifiedBy, 
        string ModifiedByUserName,
        Int64 InventoryId,
        DateTime LastModifiedDateTime

    );
}