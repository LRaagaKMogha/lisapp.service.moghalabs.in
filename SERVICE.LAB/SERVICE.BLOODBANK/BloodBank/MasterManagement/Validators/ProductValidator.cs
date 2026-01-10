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
    public class ProductValidator : BaseValidator<Product>
    {
        public ProductValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
            .Custom((x, context) =>
            {
                if (x.MinCount > 0 && x.MaxCount> 0 && x.MinCount >= x.MaxCount)
                {
                    context.AddFailure(Errors.Product.MinIsGreaterThanMax.Code, Errors.Product.MinIsGreaterThanMax.Description);
                }
            })
            .Custom((x, context) =>
            {
                if (x.MinCount > 0 && x.MaxCount > 0  && x.ThresholdCount > 0 && !(x.ThresholdCount >= x.MinCount && x.ThresholdCount <= x.MaxCount))
                {
                    context.AddFailure(Errors.Product.ThresholdShouldbeLessThanMaxCountAndGreaterThanMinCount.Code, Errors.Product.ThresholdShouldbeLessThanMaxCountAndGreaterThanMinCount.Description);
                }
            });

            RuleFor(x => x.Code)
            .Must(x =>
            {
                return x.Length is < Product.MaxCodeLength and > Product.MinCodeLength;
            }).WithErrorCode(Errors.Product.InvalidCode.Code).WithMessage(Errors.Product.InvalidCode.Description)
            .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Product.CodeInvalidCharacters.Code).WithMessage(Errors.Product.CodeInvalidCharacters.Description);


            RuleFor(x => x.Description)
            .Must(x =>
            {
                return x.Length is < Product.MaxDescriptionLength and > Product.MinDescriptionLength;
            }).WithErrorCode(Errors.Product.InvalidDescription.Code).WithMessage(Errors.Product.InvalidDescription.Description)
            .Must(x => Shared.Helpers.checkCsvVulnerableCharactersValidator(x)).WithErrorCode(Errors.Product.DescriptionInvalidCharacters.Code).WithMessage(Errors.Product.DescriptionInvalidCharacters.Description);


        }
    }
}