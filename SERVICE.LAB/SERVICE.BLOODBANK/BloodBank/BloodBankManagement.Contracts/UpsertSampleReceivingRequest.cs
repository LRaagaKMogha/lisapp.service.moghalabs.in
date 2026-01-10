using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpsertSampleReceivingRequest
    (
        List<SampleResponse> Samples
    );
}