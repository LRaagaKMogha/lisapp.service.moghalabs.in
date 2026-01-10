using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class BloodBankPatient
        {
            public static Error NotFound => Error.NotFound(
                code: "BloodBankPatient.NotFound",
                description: "BloodBank Patient not found");
        }
    }
}