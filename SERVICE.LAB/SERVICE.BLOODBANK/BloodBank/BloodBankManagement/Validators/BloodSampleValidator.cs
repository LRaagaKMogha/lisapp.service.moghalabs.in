using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.ServiceErrors;
using FluentValidation;
using Shared;

namespace BloodBankManagement.Validators
{
    public class BloodSampleValidator : BaseValidator<BloodSample>
    {
        public BloodSampleValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.BloodSample.ModifiedDetailsInCorrect.Code).WithMessage(Errors.BloodSample.ModifiedDetailsInCorrect.Description);


            RuleFor(x => x.BarCode)
                .Must(x => !string.IsNullOrEmpty(x) && x.Length == 12)
                .WithErrorCode(Errors.BloodSample.ValidBarCode.Code).WithMessage(Errors.BloodSample.ValidBarCode.Description);

            RuleFor(x => x.RegistrationId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSample.RegistrationIdRequired.Code).WithMessage(Errors.BloodSample.RegistrationIdRequired.Description);

            RuleFor(x => x.SampleTypeId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSample.SampleTypeIdRequired.Code).WithMessage(Errors.BloodSample.SampleTypeIdRequired.Description);

            RuleFor(x => x.TubeNo)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSample.TubeNoRequired.Code).WithMessage(Errors.BloodSample.TubeNoRequired.Description)
                .Must(x => HelperMethods.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.BloodSample.TubeNoInvalidCharacters.Code).WithMessage(Errors.BloodSample.TubeNoInvalidCharacters.Description);


            RuleFor(x => x.UnitCount)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSample.UnitCountRequired.Code).WithMessage(Errors.BloodSample.UnitCountRequired.Description);

            RuleFor(x => x.PatientId)
                .NotEmpty()
                .WithErrorCode(Errors.BloodSample.PatientIdRequired.Code).WithMessage(Errors.BloodSample.PatientIdRequired.Description);

        }
    }
}