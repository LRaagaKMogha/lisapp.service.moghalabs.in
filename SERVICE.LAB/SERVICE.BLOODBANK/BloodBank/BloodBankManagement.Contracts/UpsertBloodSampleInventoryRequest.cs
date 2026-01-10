using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpsertBloodSampleInventoriesRequest
    (
        List<UpsertBloodSampleInventoryRequest> BloodSampleInventories
    );

    public record UpsertBloodSampleInventoryRequest
    (
        Int64 Identifier,
        Int64 RegistrationId,
        Int64 BloodSampleResultId,
        Int64 ProductId,
        Int64 InventoryId,
        bool IsMatched,
        bool IsComplete,
        string Comments,
        string Status,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        Int64 CrossMatchingTestId,
        DateTime? CompatibilityValidTill, 
        DateTime? IssuedDateTime
    );
}