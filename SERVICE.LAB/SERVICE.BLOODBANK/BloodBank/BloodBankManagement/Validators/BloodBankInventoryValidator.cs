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
    public class BloodBankInventoryValidator : BaseValidator<BloodBankInventory>
    {
        public BloodBankInventoryValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodBankInventory.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodBankInventory.ModifiedDetailsInCorrect.Description);

            RuleFor(x => x.BatchId)
                .Must(x => string.IsNullOrEmpty(x) || HelperMethods.IsNumeric(x))
                .WithErrorCode(Errors.BloodBankInventory.ValidBatchId.Code).WithMessage(Errors.BloodBankInventory.ValidBatchId.Description);

            RuleFor(x => x.DonationId)
                .MaximumLength(13).WithErrorCode(Errors.BloodBankInventory.DonationIdMaxLength.Code).WithMessage(Errors.BloodBankInventory.DonationIdMaxLength.Description)
                .NotEmpty().WithErrorCode(Errors.BloodBankInventory.DonationIdRequired.Code).WithMessage(Errors.BloodBankInventory.DonationIdRequired.Description)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankInventory.DonationIdInvalidCharacters.Code).WithMessage(Errors.BloodBankInventory.DonationIdInvalidCharacters.Description);
                

            RuleFor(x => x.CalculatedDonationId)
                .MaximumLength(16).WithErrorCode(Errors.BloodBankInventory.CalculatedDonationIdMaxLength.Code).WithMessage(Errors.BloodBankInventory.CalculatedDonationIdMaxLength.Description)
                .NotEmpty().WithErrorCode(Errors.BloodBankInventory.CalculatedDonationIdRequired.Code).WithMessage(Errors.BloodBankInventory.CalculatedDonationIdRequired.Description)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankInventory.DonationIdInvalidCharacters.Code).WithMessage(Errors.BloodBankInventory.DonationIdInvalidCharacters.Description);

            RuleFor(x => x.ProductCode)
                .Must(x => x > 0 && GlobalConstants.Products.Any(product => product.Identifier == x))
                .WithErrorCode(Errors.BloodBankInventory.ProductCodeRequired.Code).WithMessage(Errors.BloodBankInventory.ProductCodeRequired.Description);

            RuleFor(x => x.ExpirationDateAndTime)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.DonationId))
                .WithErrorCode(Errors.BloodBankInventory.ExpirationDateAndTimeRequired.Code).WithMessage(Errors.BloodBankInventory.ExpirationDateAndTimeRequired.Description)
                .Must(HelperMethods.GreaterThanOrEqualToToday)
                .WithErrorCode(Errors.BloodBankInventory.ExpirationDateAndTimeGreaterThanToday.Code).WithMessage(Errors.BloodBankInventory.ExpirationDateAndTimeGreaterThanToday.Description);

            RuleFor(x => x.AboOnLabel)
                .Must(x => !string.IsNullOrEmpty(x) && HelperMethods.BloodGroups.Any(y => y == x))
                .WithErrorCode(Errors.BloodBankInventory.AboOnLabelRequired.Code).WithMessage(Errors.BloodBankInventory.AboOnLabelRequired.Description);

            RuleFor(x => x.Volume)
                .Must(x => !string.IsNullOrEmpty(x))
                .WithErrorCode(Errors.BloodBankInventory.VolumeRequired.Code).WithMessage(Errors.BloodBankInventory.VolumeRequired.Description)
                .Must(x => HelperMethods.IsNumeric(x))
                .WithErrorCode(Errors.BloodBankInventory.VolumeIsNumber.Code).WithMessage(Errors.BloodBankInventory.VolumeIsNumber.Description);

            RuleFor(x => x.Status)
                .Must(x => !string.IsNullOrEmpty(x) && HelperMethods.InventoryStatuses.Any(y => y == x))
                .WithErrorCode(Errors.BloodBankInventory.ValidStatus.Code).WithMessage(Errors.BloodBankInventory.ValidStatus.Description);

            RuleFor(x => x.AntiAGrading)
                .Must(x => string.IsNullOrEmpty(x) || HelperMethods.Gradings.Any(y => y == x))
                .WithErrorCode(Errors.BloodBankInventory.ValidAntiAGrading.Code).WithMessage(Errors.BloodBankInventory.ValidAntiAGrading.Description);

            RuleFor(x => x.AntiBGrading)
                .Must(x => string.IsNullOrEmpty(x) || HelperMethods.Gradings.Any(y => y == x))
                .WithErrorCode(Errors.BloodBankInventory.ValidAntiBGrading.Code).WithMessage(Errors.BloodBankInventory.ValidAntiBGrading.Description);

            RuleFor(x => x.AntiABGrading)
                .Must(x => string.IsNullOrEmpty(x) || HelperMethods.Gradings.Any(y => y == x))
                .WithErrorCode(Errors.BloodBankInventory.ValidAntiABGrading.Code).WithMessage(Errors.BloodBankInventory.ValidAntiABGrading.Description);

            RuleFor(x => x.AboResult)
                .Must(x => string.IsNullOrEmpty(x) || HelperMethods.AboResult.Any(result => result == x))
                .WithErrorCode(Errors.BloodBankInventory.ValidAboResult.Code).WithMessage(Errors.BloodBankInventory.ValidAboResult.Description);

            RuleFor(x => x.Temprature)
                .Must(x => !string.IsNullOrEmpty(x) && HelperMethods.IsNumeric(x))
                .WithErrorCode(Errors.BloodBankInventory.ValidTemperature.Code).WithMessage(Errors.BloodBankInventory.ValidTemperature.Description);


        }
    }
}