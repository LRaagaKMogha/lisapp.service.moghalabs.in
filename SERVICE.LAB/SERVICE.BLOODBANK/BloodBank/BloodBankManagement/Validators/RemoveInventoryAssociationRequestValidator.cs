using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.ServiceErrors;
using ErrorOr;
using FluentValidation;
using Shared;

namespace BloodBankManagement.Validators
{
    public class RemoveInventoryAssociationRequestValidator : BaseValidator<RemoveInventoryAssociationRequest>
    {
        public RemoveInventoryAssociationRequestValidator(HttpContext httpContext): base(httpContext)
        {
              RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodSampleInventory.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodSampleInventory.ModifiedDetailsInCorrect.Description);

            RuleFor(x => x.RegistrationId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSampleInventory.RegistrationIdRequired.Code).WithMessage(Errors.BloodSampleInventory.RegistrationIdRequired.Description);                

            RuleFor(x => x.InventoryId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSampleInventory.InventoryIdIsRequired.Code).WithMessage(Errors.BloodSampleInventory.InventoryIdIsRequired.Description);                

            
        }
    }
}