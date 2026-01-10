using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.ServiceErrors;
using ErrorOr;
using FluentValidation;
using Shared;

namespace BloodBankManagement.Validators
{
    public class UpdateBloodSampleInventoryStatusRequestValidator : BaseValidator<UpdateBloodSampleInventoryStatusRequest>
    {
        public UpdateBloodSampleInventoryStatusRequestValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x.IssuingComments)
                 .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x ?? "")).WithErrorCode(Errors.BloodSampleInventory.CommentsInvalidCharacters.Code).WithMessage(Errors.BloodSampleInventory.CommentsInvalidCharacters.Description);
            
            RuleForEach(x => x.BloodSampleInventories)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodSampleInventory.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodSampleInventory.ModifiedDetailsInCorrect.Description)
                .Must(x => x.RegistrationId > 0)
                .WithErrorCode(Errors.BloodSampleInventory.RegistrationIdRequired.Code).WithMessage(Errors.BloodSampleInventory.RegistrationIdRequired.Description)
                .Must(x => x.InventoryId > 0)
                .WithErrorCode(Errors.BloodSampleInventory.InventoryIdIsRequired.Code).WithMessage(Errors.BloodSampleInventory.InventoryIdIsRequired.Description)
                .Must(x => !string.IsNullOrEmpty(x.Status) && HelperMethods.BloodSampleInventoryStatuses.Any(y => y == x.Status))
                .WithErrorCode(Errors.BloodSampleInventory.ValidStatus.Code).WithMessage(Errors.BloodSampleInventory.ValidStatus.Description)
                .Must(x => string.IsNullOrEmpty(x.Temperature) || HelperMethods.IsNumeric(x.Temperature))
                .WithErrorCode(Errors.BloodSampleInventory.ValidTemperature.Code).WithMessage(Errors.BloodSampleInventory.ValidTemperature.Description)
                .Must(x => string.IsNullOrEmpty(x.TransfusionVolume) || HelperMethods.IsNumeric(x.TransfusionVolume))
                .WithErrorCode(Errors.BloodSampleInventory.ValidTransfusionVolume.Code).WithMessage(Errors.BloodSampleInventory.ValidTransfusionVolume.Description)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x.TransfusionComments ?? "")).WithErrorCode(Errors.BloodSampleInventory.CommentsInvalidCharacters.Code).WithMessage(Errors.BloodSampleInventory.CommentsInvalidCharacters.Description);
                



        }
    }
}