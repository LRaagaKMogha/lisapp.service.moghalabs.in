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
    public class UpsertBloodBankInventoryStatusRequestValidator : BaseValidator<UpsertBloodBankInventoryStatusRequest>
    {
        public UpsertBloodBankInventoryStatusRequestValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodBankInventory.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodBankInventory.ModifiedDetailsInCorrect.Description);

            RuleFor(x => x.InventoryId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodBankInventory.UpdateInventoryIdIsRequired.Code).WithMessage(Errors.BloodBankInventory.UpdateInventoryIdIsRequired.Description);

            RuleFor(x => x.Status)
                .Must(x => HelperMethods.InventoryStatuses.Any(y => y == x))
                .WithErrorCode(Errors.BloodBankInventory.UpdateValidStatus.Code).WithMessage(Errors.BloodBankInventory.UpdateValidStatus.Description);

            RuleFor(x => x.Comments)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodBankInventory.CommentsInvalidCharacters.Code).WithMessage(Errors.BloodBankInventory.CommentsInvalidCharacters.Description);
                            
        }
    }
}