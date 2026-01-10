using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpsertInventoriesRequest
    (
        List<UpsertInventoryRequest> Inventories
    );
    
    public record UpsertInventoryRequest
    (
        Int64 RegistrationId, 
        Int64 ProductId,
        bool IsRedCells,
        List<UpsertInventoryDetailRequest> InventoriesData,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime
    );

    public record UpsertInventoryDetailRequest 
    (
        Int64 InventoryId,
        string Comments,
        Int64 ModifiedProductId,
        DateTime? CompatibilityValidTill,
        DateTime? IssuedDateTime
    );
}