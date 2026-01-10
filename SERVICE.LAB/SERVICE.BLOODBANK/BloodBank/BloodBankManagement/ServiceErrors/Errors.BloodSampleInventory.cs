using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.ServiceErrors
{

    public static partial class Errors
    {
        public static class BloodSampleInventory
        {
            public static Error ModifiedDetailsInCorrect => Error.NotFound(code: "BloodSampleInventory.ModifiedDetails.Valid", description: "Please enter valid Modified By User Information.");
            public static Error BloodSampleResultIdRequired => Error.NotFound(code: "BloodSampleInventory.BloodSampleResultId.Required", description: "Please enter a valid BloodSampleResultId.");
            public static Error RegistrationIdRequired => Error.NotFound(code: "BloodSampleInventory.RegistrationId.Required", description: "Please Provide a valid Registration Id");
            public static Error InventoryIdIsRequired => Error.NotFound(code: "BloodSampleInventory.InventoryId.Required", description: "Inventory Id is required. Please enter a valid Inventory Id.");
            public static Error ProductIdRequired => Error.NotFound(code: "BloodSampleInventory.ProductId.Required", description: "Please enter a valid Product Identifier");
            public static Error ValidCrossMatchingTestId => Error.NotFound(code: "BloodSampleInventory.CrossMatchingTestId.Valid", description: "Please enter a valid CrossMatchingTestId");
            public static Error ValidStatus => Error.NotFound(code: "BloodSampleInventory.Status.Valid", description: "Please enter a valid Status value.");
            public static Error ValidTemperature => Error.NotFound(code: "BloodSampleInventory.Temperature.Valid", description: "Please enter a valid Temperature value.");
            public static Error ValidTransfusionVolume => Error.NotFound(code: "BloodSampleInventory.TransfusionVolume.Valid", description: "Please enter a valid TransfusionVolume value.");
            public static Error CommentsInvalidCharacters => Error.NotFound(code: "BloodSampleInventory.Comments.Invalid", description: "Comments contains Invalid text.");

        }
    }
}