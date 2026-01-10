using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record CreateBloodBankOrderLISRequest
    (
        Int64 OrderId,
        string Action,
        Int64? PatientVisitNo,
        string? VisitId
    );
}
