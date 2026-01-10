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
    public class BloodSampleInventoryValidator : BaseValidator<BloodSampleInventory>
    {
        public BloodSampleInventoryValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodSampleInventory.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodSampleInventory.ModifiedDetailsInCorrect.Description);

            // RuleFor(x => x.BloodSampleResultId)
            //     .NotEmpty()
            //     .WithErrorCode(Errors.BloodSampleInventory.BloodSampleResultIdRequired.Code).WithMessage(Errors.BloodSampleInventory.BloodSampleResultIdRequired.Description);

            RuleFor(x => x.RegistrationId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSampleInventory.RegistrationIdRequired.Code).WithMessage(Errors.BloodSampleInventory.RegistrationIdRequired.Description);                

            RuleFor(x => x.InventoryId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSampleInventory.InventoryIdIsRequired.Code).WithMessage(Errors.BloodSampleInventory.InventoryIdIsRequired.Description);                

            RuleFor(x => x.ProductId)
                .Must(x => x > 0 && GlobalConstants.Products.Any(product => product.Identifier == x))
                .WithErrorCode(Errors.BloodSampleInventory.ProductIdRequired.Code).WithMessage(Errors.BloodSampleInventory.ProductIdRequired.Description);  

            RuleFor(x => x.Status)
                .Must(x => !string.IsNullOrEmpty(x) && HelperMethods.BloodSampleInventoryStatuses.Any(y => y == x))
                .WithErrorCode(Errors.BloodSampleInventory.ValidStatus.Code).WithMessage(Errors.BloodSampleInventory.ValidStatus.Description);

            RuleFor(x => x.CrossMatchingTestId)
                .Must(x => 
                {
                    return x == GlobalConstants.CrossMatchingXMId || x == GlobalConstants.CrossMatchingManualXMId || x == GlobalConstants.CrossMatchingImmediateSpinXMId;
                })
                .WithErrorCode(Errors.BloodSampleInventory.ValidCrossMatchingTestId.Code).WithMessage(Errors.BloodSampleInventory.ValidCrossMatchingTestId.Description);                

        }
    }
}