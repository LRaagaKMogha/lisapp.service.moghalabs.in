using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class BloodSampleResult
        {
            public static Error ModifiedDetailsInCorrect => Error.NotFound(code: "BloodSampleResult.ModifiedDetails.Valid", description: "Please enter valid Modified By User Information.");
            public static Error RegistrationIdRequired => Error.NotFound(code: "BloodSampleResult.RegistrationId.Required", description: "Please Provide a valid Registration Id");
            public static Error TestDetailsNotPresent => Error.NotFound(code: "BloodSampleResult.TestDetails.NotPresent", description: "Test Information are incorrect. Please enter the correct information");
            public static Error InventoryIdIsRequired => Error.NotFound(code: "BloodSampleResult.InventoryId.Required", description: "Inventory Id is required. Please enter a valid Inventory Id.");
            public static Error StatusIsValid => Error.NotFound(code: "BloodSampleResult.Status.Valid", description: "Please enter a valid Status.");
            public static Error CommentsInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.Comments.Invalid", description: "Comments contains Invalid text.");

        }
    }
}