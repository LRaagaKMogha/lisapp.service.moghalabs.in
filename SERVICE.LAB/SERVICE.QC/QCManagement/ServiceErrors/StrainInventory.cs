using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace QCManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class StrainInventory
        {
            public static Error NotFound => Error.NotFound(code: "StrainInventory.NotFound", description: "StrainInventory not found");
            public static Error ModifiedDetailsInCorrect => Error.NotFound(code: "QC.ModifiedDetails.Valid", description: "Please enter valid Modified By User Information.");

        }
    }
}