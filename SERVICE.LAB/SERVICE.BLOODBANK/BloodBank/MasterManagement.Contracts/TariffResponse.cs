using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record Tariff
    (
        Int64 Identifier,
        Int64 ProductId,
        Int64 ResidenceId,
        decimal MRP,        
        bool IsActive,
        DateTime LastModifiedDateTime,
        string ServiceType
    );
}