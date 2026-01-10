using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace MasterManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class Lookup
        {
            public static Error InvalidName => Error.Validation(
                code: "Lookup.InvalidName",
                description: $"Lookup name must be at least {Models.Lookup.MinNameLength}" +
                    $" characters long and at most {Models.Lookup.MaxNameLength} characters long.");
            public static Error InvalidCode => Error.Validation(
                code: "Lookup.InvalidCode",
                description: $"Lookup Code must be at least {Models.Lookup.MinNameLength}" +
                    $" characters long and at most {Models.Lookup.MaxNameLength} characters long.");

            public static Error InvalidDescription => Error.Validation(
                code: "BusinessLine.InvalidDescription",
                description: $"Lookup description must be at least {Models.Lookup.MinDescriptionLength}" +
                    $" characters long and at most {Models.Lookup.MaxDescriptionLength} characters long.");

            public static Error NotFound => Error.NotFound(
                code: "Lookup.NotFound",
                description: "Lookup not found");

            public static Error LookupExists => Error.Validation(
                code: "Lookup.Exists",
                description: "Lookup Exists with Same Name Or Code");

            public static Error NotAuthorized => Error.Validation(
             code: "Lookup.NotAuthorized",
             description: "Lookup Not Authorized with Same Name Or Code");

            public static Error NameInvalidCharacters => Error.NotFound(code: "Lookups.Name.Invalid", description: "Name contains Invalid text.");
            public static Error CodeInvalidCharacters => Error.NotFound(code: "Lookups.Code.Invalid", description: "Code contains Invalid text.");

            public static Error DescriptionInvalidCharacters => Error.NotFound(code: "Lookups.Description.Invalid", description: "Description contains Invalid text.");

        }
    }
}