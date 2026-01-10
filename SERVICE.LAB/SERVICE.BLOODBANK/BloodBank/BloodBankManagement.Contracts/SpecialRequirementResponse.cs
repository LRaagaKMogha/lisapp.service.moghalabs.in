using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record SpecialRequirementResponse
    (
        Int64 Identifier,
        Int64 SpecialRequirementId,
        DateTime Validity,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        Int64 RegistrationId
    );
}