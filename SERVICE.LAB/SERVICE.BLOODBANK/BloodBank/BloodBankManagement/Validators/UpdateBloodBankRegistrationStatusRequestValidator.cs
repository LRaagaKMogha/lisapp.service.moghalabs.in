using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.ServiceErrors;
using FluentValidation;
using Shared;

namespace BloodBankManagement.Validators
{
    public class UpdateBloodBankRegistrationStatusRequestValidator : BaseValidator<UpdateBloodBankRegistrationStatusRequest>
    {
        public UpdateBloodBankRegistrationStatusRequestValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleForEach(x => x.Registrations)
                .Must(x =>
                 {
                     var user = httpContext.Items["User"] as User;
                     return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                 }).WithErrorCode(Errors.BloodBankRegistration.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodBankRegistration.ModifiedDetailsInCorrect.Description)
                 .Must(x =>
                 {
                     return x.RegistrationId > 0;
                 }).WithErrorCode(Errors.BloodBankRegistration.RegistrationIdRequired.Code).WithMessage(Errors.BloodBankRegistration.RegistrationIdRequired.Description)
                 .Must(x => !string.IsNullOrEmpty(x.RegistrationStatus) && HelperMethods.RegistrationStatus.Any(y => x.RegistrationStatus == y)).WithErrorCode(Errors.BloodBankRegistration.StatusIncorrect.Code).WithMessage(Errors.BloodBankRegistration.StatusIncorrect.Description)
                 .Must(x => x.RejectedReason == "-" || HelperMethods.checkCsvVulnerableCharactersValidator(x.RejectedReason ?? "")).WithErrorCode(Errors.BloodBankRegistration.CommentsInvalidCharacters.Code).WithMessage(Errors.BloodBankRegistration.CommentsInvalidCharacters.Description)
                 .Must(x =>
                 {
                     if (x.RegistrationStatus == "Rejected")
                         return !string.IsNullOrEmpty(x.RejectedReason);
                     return true;
                 }).WithErrorCode(Errors.BloodBankRegistration.RejectedReasonRequired.Code).WithMessage(Errors.BloodBankRegistration.RejectedReasonRequired.Description);




        }
    }
}