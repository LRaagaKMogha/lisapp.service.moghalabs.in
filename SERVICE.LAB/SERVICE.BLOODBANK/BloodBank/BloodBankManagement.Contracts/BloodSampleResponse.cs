using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodSample
    (
        List<SampleResponse> Samples
    );
}