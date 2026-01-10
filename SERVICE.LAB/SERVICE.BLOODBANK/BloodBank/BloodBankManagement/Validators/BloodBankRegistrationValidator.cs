using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.ServiceErrors;
using BloodBankManagement.Services.StartupServices;
using FluentValidation;
using Shared;

namespace BloodBankManagement.Validators
{
    public class BloodBankRegistrationValidator : BaseValidator<BloodBankRegistration>
    {

        public BloodBankRegistrationValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    return x.IndicationOfTransfusionId > 0 || !string.IsNullOrEmpty(x.IndicationOfTransfusionOthers);
                })
                .WithErrorCode(Errors.BloodBankRegistration.TransfusionIndicationRequired.Code).WithMessage(Errors.BloodBankRegistration.TransfusionIndicationRequired.Description)

                .Must(x => x.Products.Count > 0 || x.Results.Count > 0).WithErrorCode(Errors.BloodBankRegistration.ProductsOrTestsCount.Code).WithMessage(Errors.BloodBankRegistration.ProductsOrTestsCount.Description)
                .Must(x => HelperMethods.RegistrationStatus.Any(y => x.Status == y)).WithErrorCode(Errors.BloodBankRegistration.StatusIncorrect.Code).WithMessage(Errors.BloodBankRegistration.StatusIncorrect.Description)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodBankRegistration.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodBankRegistration.ModifiedDetailsInCorrect.Description)
                //.Must(x =>
                //{
                //    return !((x.WardId == null || x.WardId == 0) && (x.ClinicId == null || x.ClinicId == 0));
                //}).WithErrorCode(Errors.BloodBankRegistration.LocationOrClinicIsRequired.Code).WithMessage(Errors.BloodBankRegistration.LocationOrClinicIsRequired.Description)
                .Must(x =>
                {
                    var productIds = x.Products.Select(product => product.ProductId);
                    return productIds.All(id => GlobalConstants.Products.Any(product => product.Identifier == id));
                }).WithErrorCode(Errors.BloodBankRegistration.ProductIdNotPresent.Code).WithMessage(Errors.BloodBankRegistration.ProductIdNotPresent.Description)
                .Must(x =>
                {
                    return x.Products.All(product => product.Unit > 0 && product.ProductId > 0);
                }).WithErrorCode(Errors.BloodBankRegistration.ProductDetailsNotPresent.Code).WithMessage(Errors.BloodBankRegistration.ProductDetailsNotPresent.Description);

            RuleFor(x => x.Results)
                .Must(x =>
                {
                    var isParentTestIdCorrect = x.All(parentTest => (parentTest.ParentTestId == 0 && GlobalConstants.Tests.Any(row => row.TestNo == parentTest.TestId)) || (parentTest.ParentTestId != 0 && GlobalConstants.Tests.Any(row => row.TestNo == parentTest.ParentTestId)));
                    var isTestIdCorrect = x.Where(test => test.ParentTestId != 0).All(test => GlobalConstants.SubTests.Any(row => row.SubTestNo == test.TestId));
                    var testNameIsCorrect = x.All(test => !string.IsNullOrEmpty(test.TestName));
                    var statusIsCorrect = x.All(test => HelperMethods.RegistrationStatus.Any(y => test.Status == y));
                    var modifiedDetailsIsCorrect = x.All(test => test.ModifiedBy != 0 && !string.IsNullOrEmpty(test.ModifiedByUserName));
                    return isTestIdCorrect && isParentTestIdCorrect && testNameIsCorrect && statusIsCorrect && modifiedDetailsIsCorrect;
                }).WithErrorCode(Errors.BloodBankRegistration.TestDetailsNotPresent.Code).WithMessage(Errors.BloodBankRegistration.TestDetailsNotPresent.Description);

            RuleFor(x => x.SpecialRequirements)
                .Must(x =>
                {
                    var isSpecialRequirementIdCorrect = x.All(req => req.SpecialRequirementId != 0 && GlobalConstants.Lookups.Any(lookup => lookup.Type == "specialrequirement" && lookup.Identifier == req.SpecialRequirementId));
                    var isValidityPresent = x.All(req => req.Validity != DateTime.MinValue);
                    var modifiedDetailsIsCorrect = x.All(test => test.ModifiedBy != 0 && !string.IsNullOrEmpty(test.ModifiedByUserName));
                    return isSpecialRequirementIdCorrect && isValidityPresent && modifiedDetailsIsCorrect;
                }).WithErrorCode(Errors.BloodBankRegistration.SpecialRequirementsDetailsNotPresent.Code).WithMessage(Errors.BloodBankRegistration.SpecialRequirementsDetailsNotPresent.Description);


            RuleFor(x => x.NRICNumber)
                .NotEmpty().WithErrorCode(Errors.BloodBankRegistration.NRICNumberRequired.Code).WithMessage(Errors.BloodBankRegistration.NRICNumberRequired.Description)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankRegistration.NricNumberInvalidCharacters.Code).WithMessage(Errors.BloodBankRegistration.NricNumberInvalidCharacters.Description)
                .MaximumLength(13).WithErrorCode(Errors.BloodBankRegistration.NRICNumberMaxLength.Code).WithMessage(Errors.BloodBankRegistration.NRICNumberMaxLength.Description);

            RuleFor(x => x.PatientName)
                .NotEmpty().WithErrorCode(Errors.BloodBankRegistration.PatientNameRequired.Code).WithMessage(Errors.BloodBankRegistration.PatientNameRequired.Description)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankRegistration.PatientNameInvalidCharacters.Code).WithMessage(Errors.BloodBankRegistration.PatientNameInvalidCharacters.Description);

            RuleFor(x => x.PatientDOB)
                .NotEmpty().WithErrorCode(Errors.BloodBankRegistration.PatientNameRequired.Code).WithMessage(Errors.BloodBankRegistration.PatientNameRequired.Description)
                .Must(HelperMethods.LessThanOrEqualToToday).WithErrorCode(Errors.BloodBankRegistration.PatientDOBValidDate.Code).WithMessage(Errors.BloodBankRegistration.PatientDOBValidDate.Description)
                .Must(HelperMethods.CalculateAge).WithErrorCode(Errors.BloodBankRegistration.PatientDOBAgeLimit.Code).WithMessage(Errors.BloodBankRegistration.PatientDOBAgeLimit.Description);

            RuleFor(x => x.GenderId)
                .NotEmpty().WithErrorCode(Errors.BloodBankRegistration.GenderRequired.Code).WithMessage(Errors.BloodBankRegistration.GenderRequired.Description)
                .Must(x => HelperMethods.IsPresentIntheLookup(x, "gender")).WithErrorCode(Errors.BloodBankRegistration.GenderIdNotPresent.Code).WithMessage(Errors.BloodBankRegistration.GenderIdNotPresent.Description);

            RuleFor(x => x.NationalityId)
                .NotEmpty().WithErrorCode(Errors.BloodBankRegistration.NationalityRequired.Code).WithMessage(Errors.BloodBankRegistration.NationalityRequired.Description)
                .Must(x => HelperMethods.IsPresentIntheLookup(x, "nationality")).WithErrorCode(Errors.BloodBankRegistration.NationalityIdNotPresent.Code).WithMessage(Errors.BloodBankRegistration.NationalityIdNotPresent.Description);

            RuleFor(x => x.RaceId)
                .Must(x => x == 0 || HelperMethods.IsPresentIntheLookup(x, "race")).WithErrorCode(Errors.BloodBankRegistration.RaceIdNotPresent.Code).WithMessage(Errors.BloodBankRegistration.RaceIdNotPresent.Description);

            RuleFor(x => x.ResidenceStatusId)
                .NotEmpty().WithErrorCode(Errors.BloodBankRegistration.ResidenceRequired.Code).WithMessage(Errors.BloodBankRegistration.ResidenceRequired.Description)
                .Must(x => HelperMethods.IsPresentIntheLookup(x, "residence")).WithErrorCode(Errors.BloodBankRegistration.ResidenceIdNotPresent.Code).WithMessage(Errors.BloodBankRegistration.ResidenceIdNotPresent.Description);

            RuleFor(x => x.ClinicalDiagnosisId)
                .Must(x => x.HasValue && x > 0 ? HelperMethods.IsPresentIntheLookup(x.Value, "clinicaldiagnosis") : true).WithErrorCode(Errors.BloodBankRegistration.ClinicalDiagnosisIdNotPresent.Code).WithMessage(Errors.BloodBankRegistration.ClinicalDiagnosisIdNotPresent.Description);

            RuleFor(x => x.IndicationOfTransfusionId)
              .Must(x => x == 0 || HelperMethods.IsPresentIntheLookup(x, "transfusionindicator")).WithErrorCode(Errors.BloodBankRegistration.TransfusionIndicationIdNotPresent.Code).WithMessage(Errors.BloodBankRegistration.TransfusionIndicationIdNotPresent.Description);

            RuleFor(x => x.CaseOrVisitNumber)
                .NotEmpty().WithErrorCode(Errors.BloodBankRegistration.CaseNumberIndicationRequired.Code).WithMessage(Errors.BloodBankRegistration.CaseNumberIndicationRequired.Description)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankRegistration.CaseNumberInvalidCharacters.Code).WithMessage(Errors.BloodBankRegistration.CaseNumberInvalidCharacters.Description)
                .MaximumLength(20).WithErrorCode(Errors.BloodBankRegistration.CaseNumberMaxLength.Code).WithMessage(Errors.BloodBankRegistration.CaseNumberMaxLength.Description);

            RuleFor(x => x.ClinicalDiagnosisOthers)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankRegistration.ClinicalDiagnosisOthersInvalidCharacters.Code).WithMessage(Errors.BloodBankRegistration.ClinicalDiagnosisOthersInvalidCharacters.Description);

            RuleFor(x => x.IndicationOfTransfusionOthers)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankRegistration.TransfusionIndicatorOthersInvalidCharacters.Code).WithMessage(Errors.BloodBankRegistration.TransfusionIndicatorOthersInvalidCharacters.Description);

        }
    }
}