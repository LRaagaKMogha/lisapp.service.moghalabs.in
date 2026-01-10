using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using QCManagement.Models;
using Shared;

namespace QCManagement.Validators
{
    public class MicroQCMasterValidator : BaseValidator<MicroQCMaster>
    {
        public MicroQCMasterValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return user == null || (x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName);
                })
                .WithMessage("The modifiedBy and modifiedByUserName fields must match the authenticated user.");

        }
    }
}
