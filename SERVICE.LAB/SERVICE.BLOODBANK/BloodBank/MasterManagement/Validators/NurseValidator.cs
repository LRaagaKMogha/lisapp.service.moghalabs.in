using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MasterManagement.Models;
using MasterManagement.ServiceErrors;
using Shared;

namespace MasterManagement.Validators
{
    public class NurseValidator : BaseValidator<Nurse>
    {
        public NurseValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x.Name)
            .Must(x =>
            {
                return x.Length is < Nurse.MaxNameLength and > Nurse.MinNameLength;
            }).WithErrorCode(Errors.Nurse.InvalidName.Code).WithMessage(Errors.Nurse.InvalidName.Description)
            .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Nurse.NameInvalidCharacters.Code).WithMessage(Errors.Nurse.NameInvalidCharacters.Description);

            RuleFor(x => x.EmployeeId)
                .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Nurse.EmployeeInvalidCharacters.Code).WithMessage(Errors.Nurse.EmployeeInvalidCharacters.Description);

            RuleFor(x => x.Description)
            .Must(x =>
            {
                return x.Length is < Nurse.MaxDescriptionLength and > Nurse.MinDescriptionLength;
            }).WithErrorCode(Errors.Nurse.InvalidDescription.Code).WithMessage(Errors.Nurse.InvalidDescription.Description)
            .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Nurse.DescriptionInvalidCharacters.Code).WithMessage(Errors.Nurse.DescriptionInvalidCharacters.Description);


        }
    }
}