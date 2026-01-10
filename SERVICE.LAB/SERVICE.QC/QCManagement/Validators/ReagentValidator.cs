using FluentValidation;
using QCManagement.Models;
using Shared;
using QCManagement.ServiceErrors;

namespace QCManagement.Validators
{
    public class ReagentValidator : BaseValidator<Reagent>
    {
        public ReagentValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)
            .Must(x =>
            {
                var user = httpContext.Items["User"] as User;
                return user == null || (x.ModifiedBy != 0 && x.ModifiedBy == user!.UserNo && !string.IsNullOrEmpty(x.ModifiedByUserName) && x.ModifiedByUserName == user!.UserName);
            }).WithErrorCode(Errors.Reagent.ModifiedDetailsInCorrect.Code).WithMessage(Errors.Reagent.ModifiedDetailsInCorrect.Description);

        }
    }
}
