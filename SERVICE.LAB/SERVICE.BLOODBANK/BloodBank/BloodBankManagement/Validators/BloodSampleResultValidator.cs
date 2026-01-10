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
    public class BloodSampleResultValidator : BaseValidator<BloodSampleResult>
    {
        public BloodSampleResultValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodSampleResult.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodSampleResult.ModifiedDetailsInCorrect.Description)
                .Must(x =>
                {
                    var isParentTestIdCorrect = (x.ParentTestId == 0 && GlobalConstants.Tests.Any(row => row.TestNo == x.TestId && row.TestName == x.TestName)) || (x.ParentTestId != 0 && GlobalConstants.Tests.Any(row => row.TestNo == x.ParentTestId && row.TestName == x.TestName));
                    var isTestIdCorrect = true; 
                    if(x.ParentTestId != 0) isParentTestIdCorrect =  GlobalConstants.SubTests.Any(row => row.TestNo == x.ParentTestId && row.SubTestNo == x.TestId && row.SubTestName == x.TestName);
                    return isParentTestIdCorrect && isTestIdCorrect;
                }).WithErrorCode(Errors.BloodSampleResult.TestDetailsNotPresent.Code).WithMessage(Errors.BloodSampleResult.TestDetailsNotPresent.Description);

            RuleFor(x => x.BloodBankRegistrationId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSampleResult.RegistrationIdRequired.Code).WithMessage(Errors.BloodSampleResult.RegistrationIdRequired.Description);                

            //RuleFor(x => x.InventoryId)
            //    .NotEmpty()
            //    .WithErrorCode(Errors.BloodSampleResult.InventoryIdIsRequired.Code).WithMessage(Errors.BloodSampleResult.InventoryIdIsRequired.Description);                
                
            RuleFor(x => x.Status)
                .Must(x => HelperMethods.BloodSampleResultStatuses.Any(y => x == y))
                .WithErrorCode(Errors.BloodSampleResult.StatusIsValid.Code).WithMessage(Errors.BloodSampleResult.StatusIsValid.Description);                
                
            RuleFor(x => x.Comments)
                .Must(x => x == "-" || HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodSampleResult.CommentsInvalidCharacters.Code).WithMessage(Errors.BloodSampleResult.CommentsInvalidCharacters.Description);

        }
    }
}