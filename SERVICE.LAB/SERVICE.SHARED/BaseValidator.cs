using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Shared
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected readonly HttpContext HttpContext;

        protected BaseValidator(HttpContext httpContext)
        {
            HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }
    }
}