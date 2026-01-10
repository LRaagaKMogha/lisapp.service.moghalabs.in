using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record DeleteSchedulerRequest
    (
        Int64 Identifier,
        bool DeleteBatch
    );
}