using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Shared
{
    public static class Helpers
    {
        public static List<Error> ValidateInput<TInput, TValidator>(TInput input, HttpContext httpContext)
            where TValidator : AbstractValidator<TInput>
        {
            var errors = new List<Error>();
            Type validatorType = typeof(TValidator);
            var constructorWithHttpContext = validatorType.GetConstructor(new[] { typeof(HttpContext) });
            if (constructorWithHttpContext == null)
            {
                throw new InvalidOperationException($"TValidator must have a constructor that accepts an HttpContext parameter.");
            }
            var validator = (TValidator)constructorWithHttpContext.Invoke(new object[] { httpContext });
            var validationResult = validator.Validate(input);
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors.Select(x => Error.Validation(x.ErrorCode, x.ErrorMessage)));
            }
            return errors;
        }

        public static bool checkCsvVulnerableCharactersValidator(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;
            Regex regex = new Regex(@"^(=|\+|\-|@)");
            return !regex.IsMatch(input);
        }
    }
}