using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BloodBankManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class BloodBankRegistration
        {
            public static Error NotFound => Error.NotFound(code: "PatientRegistration.NotFound", description: "Patient Registration not found");
            public static Error NRICNumberRequired => Error.NotFound(code: "BloodBankRegistration.NRICNumber.Required", description: "NRICNumber is required");
            public static Error PatientNameRequired => Error.NotFound(code: "BloodBankRegistration.PatientName.Required", description: "PatientName is required");
            public static Error PatientDOBRequired => Error.NotFound(code: "BloodBankRegistration.PatientDOB.Required", description: "PatientDOB is required");
            public static Error GenderRequired => Error.NotFound(code: "BloodBankRegistration.Gender.Required", description: "Gender is required");
            public static Error NationalityRequired => Error.NotFound(code: "BloodBankRegistration.Nationality.Required", description: "Nationality is required");
            public static Error ResidenceRequired => Error.NotFound(code: "BloodBankRegistration.Residence.Required", description: "Residence is required");
            public static Error TransfusionIndicationRequired => Error.NotFound(code: "BloodBankRegistration.TransfusionIndication.Required", description: "TransfusionIndication is required");
            public static Error TypeOfRequestIndicationRequired => Error.NotFound(code: "BloodBankRegistration.TypeOfRequest.Required", description: "TypeOfRequest is required");
            public static Error CaseNumberIndicationRequired => Error.NotFound(code: "BloodBankRegistration.CaseNumber.Required", description: "CaseNumber is required");
            public static Error NRICNumberMaxLength => Error.NotFound(code: "BloodBankRegistration.NRICNumber.MaxLength", description: "NRICNumber Maximum length is 13 characters");
            public static Error ProductsOrTestsCount => Error.NotFound(code: "BloodBankRegistration.ProductsOrTests.Count", description: "Please add atleast one Product/Test to proceed with Registration.");
            public static Error LocationOrClinicIsRequired => Error.NotFound(code: "BloodBankRegistration.LocationOrClinic.Required", description: "Either Location or Clinic has to be entered.");
            public static Error CaseNumberMaxLength => Error.NotFound(code: "BloodBankRegistration.CaseOrVisitNumber.MaxLength", description: "CaseOrVisitNumber Maximum length is 20 characters");
            public static Error PatientDOBValidDate => Error.NotFound(code: "BloodBankRegistration.PatientDOB.Valid", description: "Patient DOB should be less than or equal to today");
            public static Error PatientDOBAgeLimit => Error.NotFound(code: "BloodBankRegistration.PatientDOB.AgeLimit", description: "Patient DOB age should be with in 120 years");
            public static Error GenderIdNotPresent => Error.NotFound(code: "BloodBankRegistration.Gender.IdNotPresent", description: "Gender Id is not present in the Lookup");
            public static Error RaceIdNotPresent => Error.NotFound(code: "BloodBankRegistration.Race.IdNotPresent", description: "Race Id is not present in the Lookup");
            public static Error ClinicalDiagnosisIdNotPresent => Error.NotFound(code: "BloodBankRegistration.ClinicalDiagnosis.IdNotPresent", description: "ClinicalDiagnosis Id is not present in the Lookup");
            public static Error TransfusionIndicationIdNotPresent => Error.NotFound(code: "BloodBankRegistration.TransfusionIndication.IdNotPresent", description: "TransfusionIndication Id is not present in the Lookup");
            public static Error ProductIdNotPresent => Error.NotFound(code: "BloodBankRegistration.ProductId.IdNotPresent", description: "Product Id in the product array is not present in the Product Master");
            public static Error ProductDetailsNotPresent => Error.NotFound(code: "BloodBankRegistration.ProductDetails.NotPresent", description: "Product Details like Product Id And Unit  are not present. Please enter the requierd Product details");
            public static Error TestDetailsNotPresent => Error.NotFound(code: "BloodBankRegistration.TestDetails.NotPresent", description: "Test Information are incorrect. Please enter the correct information");
            public static Error SpecialRequirementsDetailsNotPresent => Error.NotFound(code: "BloodBankRegistration.SpecialRequirementsDetails.NotPresent", description: "SpecialRequirements Information are incorrect. Please enter the correct information");
            public static Error NationalityIdNotPresent => Error.NotFound(code: "BloodBankRegistration.Nationality.IdNotPresent", description: "Nationality Id is not present in the Lookup");
            public static Error ResidenceIdNotPresent => Error.NotFound(code: "BloodBankRegistration.Residence.IdNotPresent", description: "Residence Id is not present in the Lookup");
            public static Error StatusIncorrect => Error.NotFound(code: "BloodBankRegistration.Status.Incorrect", description: "Please pass proper Registration Status");
            public static Error ModifiedDetailsInCorrect => Error.NotFound(code: "BloodBankRegistration.ModifiedDetails.Incorrect", description: "Please pass proper Modified User details");
            public static Error RegistrationIdRequired => Error.NotFound(code: "BloodBankRegistration.RegistrationId.Required", description: "Please Provide a valid Registration Id");
            public static Error RejectedReasonRequired => Error.NotFound(code: "BloodBankRegistration.RejectedReason.Required", description: "Rejected Reason is required");

            public static Error PatientNameInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.PatientName.Invalid", description: "PatientName contains Invalid text.");
            public static Error NricNumberInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.NricNumber.Invalid", description: "NricNumber contains Invalid text.");
            public static Error ClinicalDiagnosisOthersInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.ClinicalDiagnosisOthers.Invalid", description: "ClinicalDiagnosis contains Invalid text.");
            public static Error TransfusionIndicatorOthersInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.transfusionIndicatorOthers.Invalid", description: "transfusionIndicator contains Invalid text.");
            public static Error CaseNumberInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.CaseNumber.Invalid", description: "CaseNumber contains Invalid text.");
            public static Error CommentsInvalidCharacters => Error.NotFound(code: "BloodBankRegistration.Comments.Invalid", description: "Comments contains Invalid text.");


        }
    }
}