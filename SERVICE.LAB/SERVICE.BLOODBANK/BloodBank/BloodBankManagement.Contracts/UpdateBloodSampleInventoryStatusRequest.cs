using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{

    public record UpdateBloodSampleInventoryStatusRequest
    (
        List<UpdateBloodSampleInventory> BloodSampleInventories,
        Int64? NurseId,
        string? IssuingComments,
        Int64? WardId,
        bool IsEmergency,
        bool IsAllResultsCompleted,
        bool IsTransfusionUpdatedPostCompletion
    );

    public record UpdateBloodSampleInventory
    (
        Int64 RegistrationId,
        Int64 Identifier,
        Int64 InventoryId,
        string Status,
        string? Temperature,
        string? TransfusionComments,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime? LastModifiedDateTime,
        DateTime? TransfusionDateTime,
        string? TransfusionVolume,
        bool IsTransfusionReaction,
        DateTime? CompatibilityValidTill,
        DateTime? IssuedDateTime
    );

}