using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodBankBillingResponse
    (
        Int64 Identifier,
        Int64 ProductId,
        Int64 TestId,
        Int64 ClinicId,
        string EntityId,
        decimal MRP,
        int Unit,
        decimal Price,
        string Status,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool IsBilled,
        string ServiceType
    );
}
