using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace MasterManagement.ServiceErrors
{
 public  static partial class Errors
    {
        public static class Product
        {
            public static Error InvalidCode => Error.Validation(
                code: "Product.InvalidCode",
                description: $"Product Code must be at least {Models.Product.MinCodeLength}" +
                    $" characters long and at most {Models.Product.MaxCodeLength} characters long.");

            public static Error InvalidDescription => Error.Validation(
                code: "Product.InvalidDescription",
                description: $"Product description must be at least {Models.Product.MinDescriptionLength}" +
                    $" characters long and at most {Models.Product.MaxDescriptionLength} characters long.");

            public static Error MinIsGreaterThanMax => Error.Validation(
                code: "Product.MinIsGreaterThanMax",
                description: $"Product Min Count must be  lesser than  Max Allowed Count");
            public static Error ThresholdShouldbeLessThanMaxCountAndGreaterThanMinCount => Error.Validation(
                code: "Product.ThresholdShouldbeLessThanMaxCountAndGreaterThanMinCount",
                description: $"Product ThresHold Count should be greater than Min Count  and Less than Max Count");


            public static Error NotFound => Error.NotFound(
                code: "Product.NotFound",
                description: "Product not found");

            public static Error ProductExists => Error.Validation(
                code: "Product.Exists",
                description: "Product Exists with Same Code Or Code");

            public static Error NotAuthorized => Error.Validation(
             code: "Product.NotAuthorized",
             description: "Product Not Authorized with Same Code Or Code");
            public static Error CodeInvalidCharacters => Error.NotFound(code: "Product.Code.Invalid", description: "Code contains Invalid text.");
            public static Error DescriptionInvalidCharacters => Error.NotFound(code: "Product.Description.Invalid", description: "Description contains Invalid text.");

        }
    }
}