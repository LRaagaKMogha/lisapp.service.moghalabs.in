using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchPatientHistoryRequest
    (
        string? NRICNumber,
        DateTime? StartDate,
        DateTime? EndDate,
        string? Status
    );
}