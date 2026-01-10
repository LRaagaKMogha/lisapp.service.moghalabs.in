using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class BloodBankInventory
        {
            public static Error NotFound => Error.NotFound(code: "BloodBankInventory.NotFound", description: "BloodBank Inventory not found");
            public static Error DonationIdRequired => Error.NotFound(code: "BloodBankInventory.DonationId.Required", description: "DonationId is required.");
            public static Error DonationIdMaxLength => Error.NotFound(code: "BloodBankInventory.DonationId.MaxLength", description: "DonationId Max length is 13 characters.");
            public static Error CalculatedDonationIdRequired => Error.NotFound(code: "BloodBankInventory.CalculatedDonationId.Required", description: "Calculated DonationId is required.");
            public static Error CalculatedDonationIdMaxLength => Error.NotFound(code: "BloodBankInventory.CalculatedDonationId.MaxLength", description: "Calculated DonationId Max length is 16 characters.");
            public static Error ProductCodeRequired => Error.NotFound(code: "BloodBankInventory.ProductCode.Required", description: "Please enter a valid Product Identifier");
            public static Error ExpirationDateAndTimeRequired => Error.NotFound(code: "BloodBankInventory.ExpirationDateAndTime.Required", description: "ExpirationDateAndTime is required.");
            public static Error ExpirationDateAndTimeGreaterThanToday => Error.NotFound(code: "BloodBankInventory.ExpirationDateAndTime.GreaterThanToday", description: "ExpirationDateAndTime should be greater than Today.");
            public static Error AboOnLabelRequired => Error.NotFound(code: "BloodBankInventory.AboOnLabel.Required", description: "Please enter a valid AboOnLabel value.");
            public static Error VolumeRequired => Error.NotFound(code: "BloodBankInventory.Volume.Required", description: "Volume is required.");
            public static Error VolumeIsNumber => Error.NotFound(code: "BloodBankInventory.Volume.IsNumber", description: "Volume should be a number.");
            public static Error ValidStatus => Error.NotFound(code: "BloodBankInventory.Status.Valid", description: "Please enter a valid Status value.");
            public static Error ValidAntiAGrading => Error.NotFound(code: "BloodBankInventory.AntiAGrading.Valid", description: "Please enter a valid AntiAGrading value.");
            public static Error ValidAntiBGrading => Error.NotFound(code: "BloodBankInventory.AntiAGrading.Valid", description: "Please enter a valid AntiBGrading value.");
            public static Error ValidAntiABGrading => Error.NotFound(code: "BloodBankInventory.AntiABGrading.Valid", description: "Please enter a valid AntiABGrading value.");
            public static Error ValidBatchId => Error.NotFound(code: "BloodBankInventory.BatchId.Valid", description: "BatchId should be passed as an empty value and it gets generated automatically while saving.");
            public static Error ValidAboResult => Error.NotFound(code: "BloodBankInventory.ABOResult.Valid", description: "Please enter a valid ABOResult value.");
            public static Error ValidTemperature => Error.NotFound(code: "BloodBankInventory.Temperature.Valid", description: "Please enter a valid Temperature value.");
            public static Error ModifiedDetailsInCorrect => Error.NotFound(code: "BloodBankInventory.ModifiedDetails.Valid", description: "Please enter valid Modified By User Information.");
            public static Error UpdateInventoryIdIsRequired => Error.NotFound(code: "BloodBankInventory.Update.InventoryId.Required", description: "Inventory Id is required. Please enter a valid Inventory Id.");
            public static Error UpdateValidStatus => Error.NotFound(code: "BloodBankInventory.Update.Status.Valid", description: "Please enter a valid Inventory Status.");
            public static Error DonationIdInvalidCharacters => Error.NotFound(code: "BloodBankInventory.DonationId.Invalid", description: "DonationId contains Invalid text.");
            public static Error CommentsInvalidCharacters => Error.NotFound(code: "BloodBankInventory.Comments.Invalid", description: "Comments contains Invalid text.");

        }
    }
}