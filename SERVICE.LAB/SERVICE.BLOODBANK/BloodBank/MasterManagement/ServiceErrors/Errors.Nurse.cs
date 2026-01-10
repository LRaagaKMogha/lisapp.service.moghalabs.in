using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace MasterManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class Nurse
        {
            public static Error InvalidName => Error.Validation(
                code: "Nurse.InvalidName",
                description: $"Nurse name must be at least {Models.Nurse.MinNameLength}" +
                    $" characters long and at most {Models.Nurse.MaxNameLength} characters long.");

            public static Error InvalidDescription => Error.Validation(
                code: "BusinessLine.InvalidDescription",
                description: $"Nurse description must be at least {Models.Nurse.MinDescriptionLength}" +
                    $" characters long and at most {Models.Nurse.MaxDescriptionLength} characters long.");

            public static Error NotFound => Error.NotFound(
                code: "Nurse.NotFound",
                description: "Nurse not found");

            public static Error NurseExists => Error.Validation(
                code: "Nurse.Exists",
                description: "Nurse Exists with Same Name Or EmployeeId");

            public static Error NotAuthorized => Error.Validation(
             code: "Nurse.NotAuthorized",
             description: "Nurse Not Authorized with Same Name Or EmployeeId");
            public static Error NameInvalidCharacters => Error.NotFound(code: "Nurse.Name.Invalid", description: "Name contains Invalid text.");
            public static Error DescriptionInvalidCharacters => Error.NotFound(code: "Nurse.Description.Invalid", description: "Description contains Invalid text.");
            public static Error EmployeeInvalidCharacters => Error.NotFound(code: "Nurse.Employee.Invalid", description: "Employee contains Invalid text.");

        }
    }
}