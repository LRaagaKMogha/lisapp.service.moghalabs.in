using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record RegistrationTransactionResponse
  (
        Int64 Identifier,
        string RegistrationStatus,
        string Comments,
        Int64 ModifiedBy,
        string EntityType,
        Int64? EntityId,
        string EntityAction,
        Int64 RegistrationId,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime
  );
}