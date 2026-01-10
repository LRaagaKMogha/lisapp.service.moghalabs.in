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
    public class LookupValidator : BaseValidator<Lookup>
    {
        public LookupValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x.Name)
            .Must(x =>
            {
                return x.Length is < Lookup.MaxNameLength and > Lookup.MinNameLength;
            }).WithErrorCode(Errors.Lookup.InvalidName.Code).WithMessage(Errors.Lookup.InvalidName.Description)
            .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Lookup.NameInvalidCharacters.Code).WithMessage(Errors.Lookup.NameInvalidCharacters.Description);
            RuleFor(x => x.Code)
            .Must(x =>
            {
                return x.Length is < Lookup.MaxNameLength and > Lookup.MinNameLength;
            }).WithErrorCode(Errors.Lookup.InvalidCode.Code).WithMessage(Errors.Lookup.InvalidCode.Description)
            .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Lookup.CodeInvalidCharacters.Code).WithMessage(Errors.Lookup.CodeInvalidCharacters.Description);



            RuleFor(x => x.Description)
            .Must(x =>
            {
                return x.Length is < Lookup.MaxDescriptionLength and > Lookup.MinDescriptionLength;
            }).WithErrorCode(Errors.Lookup.InvalidDescription.Code).WithMessage(Errors.Lookup.InvalidDescription.Description)
            .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Lookup.DescriptionInvalidCharacters.Code).WithMessage(Errors.Lookup.DescriptionInvalidCharacters.Description);


        }
    }
}