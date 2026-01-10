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
    public class MediaInventoryValidator : BaseValidator<MediaInventory>
    {
        public MediaInventoryValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return user == null || (x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName);
                }).WithErrorCode(Errors.MediaInventory.ModifiedDetailsInCorrect.Code).WithMessage(Errors.MediaInventory.ModifiedDetailsInCorrect.Description);

        }
    }
}