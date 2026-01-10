using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace MasterManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class Tariff
        {
            public static Error NotFound => Error.NotFound(
                code: "Tariff.NotFound",
                description: "Tariff not found");

            public static Error TariffExists => Error.Validation(
                code: "Tariff.Exists",
                description: "Tariff Exists with Same ProductId and residenceId");

            public static Error NotAuthorized => Error.Validation(
             code: "Tariff.NotAuthorized",
             description: "Tariff Not Authorized with Same ProductId and residenceId");
        }
    }
}