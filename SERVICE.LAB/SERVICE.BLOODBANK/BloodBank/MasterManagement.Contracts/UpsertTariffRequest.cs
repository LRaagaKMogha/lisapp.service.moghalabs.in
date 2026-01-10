using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record UpsertTariffsRequest(
        List<UpsertTariffRequest> Tariffs
    );

    public record UpsertTariffRequest(
        Int64 ProductId,
        Int64 ResidenceId,
        decimal MRP,
        bool IsActive,
        Int64? Id,
        string ServiceType
    );
}