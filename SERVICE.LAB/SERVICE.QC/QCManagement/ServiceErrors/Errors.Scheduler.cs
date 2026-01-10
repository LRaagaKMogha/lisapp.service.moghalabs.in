using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace QCManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class Scheduler
        {
            public static Error NotFound => Error.NotFound(code: "Scheduler.NotFound", description: "Scheduler not found");
            public static Error ModifiedDetailsInCorrect => Error.NotFound(code: "QC.ModifiedDetails.Valid", description: "Please enter valid Modified By User Information.");

        }
    }
}