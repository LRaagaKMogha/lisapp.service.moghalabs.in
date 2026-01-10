using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchPatientBillingRequest
        (
            string? NRICNumber,
            DateTime? StartDate,
            DateTime? EndDate,
            string? Status
        );
}
