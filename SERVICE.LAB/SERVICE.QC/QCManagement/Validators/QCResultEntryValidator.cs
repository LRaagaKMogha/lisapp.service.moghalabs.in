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
    public class QCResultEntryValidator : BaseValidator<QCResultEntry>
    {
        public QCResultEntryValidator(HttpContext httpContext) : base(httpContext)
        {
             RuleFor(x => x)
                .Must(x =>
                {
                    var user = httpContext.Items["User"] as User;
                    return x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName;
                }).WithErrorCode(Errors.QCResultEntry.ModifiedDetailsInCorrect.Code).WithMessage(Errors.QCResultEntry.ModifiedDetailsInCorrect.Description);

        }
    }
}