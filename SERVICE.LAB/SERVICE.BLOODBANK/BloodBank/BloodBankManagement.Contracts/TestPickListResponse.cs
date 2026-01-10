using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record TestPickListResponse
    (
        int TestPickListNo,
        int TestNo,
        int SubTestNo,
        string PickValue,
        bool IsDefault,
        bool isAbnormal,
        int SequenceNo
    );
}