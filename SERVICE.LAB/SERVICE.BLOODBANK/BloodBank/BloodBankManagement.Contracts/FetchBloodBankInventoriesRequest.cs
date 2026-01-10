using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchBloodBankInventoriesRequest
    (
        List<string>? Statuses,
        List<Int64>? InventoryIds,
        DateTime? StartDate,
        DateTime? EndDate,
        DateTime? ExpirationDate,
        string? DonationId,
        bool ExactSearch = false
    );
}