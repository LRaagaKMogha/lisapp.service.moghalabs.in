using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class BloodSample
        {
            public static Error ValidBarCode => Error.NotFound(code: "BloodSample.BarCode.Valid", description: "Please enter a valid barcode with 12 characters");
            public static Error RegistrationIdRequired => Error.NotFound(code: "BloodSample.RegistrationId.Required", description: "RegistrationId is required. Enter a valid RegistrationId");
            public static Error SampleTypeIdRequired => Error.NotFound(code: "BloodSample.SampleTypeId.Required", description: "SampleTypeId is required. Enter a valid SampleTypeId");
            public static Error TubeNoRequired => Error.NotFound(code: "BloodSample.TubeNo.Required", description: "TubeNo is required");
            public static Error ModifiedDetailsInCorrect => Error.NotFound(code: "BloodBankInventory.ModifiedDetails.Valid", description: "Please enter valid Modified By User Information.");
            public static Error UnitCountRequired => Error.NotFound(code: "BloodSample.UnitCount.Required", description: "UnitCount is required");
            public static Error PatientIdRequired => Error.NotFound(code: "BloodSample.PatientId.Required", description: "PatientId is required");
            public static Error TubeNoInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.TubeNo.Invalid", description: "TubeNo contains Invalid text.");


        }
    }
}