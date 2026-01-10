using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using QCManagement.Models;
using QCManagement.ServiceErrors;
using Shared;

namespace QCManagement.Validators
{
    public class StrainMasterValidator : BaseValidator<StrainMaster>
    {
        public StrainMasterValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return user == null || (x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName);
                }).WithErrorCode(Errors.StrainMaster.ModifiedDetailsInCorrect.Code).WithMessage(Errors.StrainMaster.ModifiedDetailsInCorrect.Description);
        }
    }
}
