using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodSampleInventoryResponse
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
        Int64? IssuedToNurseId,
        Int64? ClinicId,
        Int64? PostIssuedClinicId,
        Int64? ReturnedByNurseId,
        DateTime? TransfusionDateTime,
        string TransfusionVolume,
        bool IsTransfusionReaction,
        string TransfusionComments,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateAndTime,
        Int64 CrossMatchingTestId,
        DateTime? CompatibilityValidTill,
        DateTime? IssuedDateTime,
        BloodBankInventoryResponse? InventoryResponse = null
    );
}
