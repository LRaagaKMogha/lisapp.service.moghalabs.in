using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string role) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { new CustomAuthorizationRequirement(role) };
        }
    }

    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public CustomAuthorizationRequirement(string role)
        {
            Role = role;
        }
    }

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly CustomAuthorizationRequirement _requirement;

        public CustomAuthorizeFilter(CustomAuthorizationRequirement requirement)
        {
            _requirement = requirement;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var user = context.HttpContext.Items["User"] as User;
            var isSuccess = false;
            var roles = _requirement.Role?.Split(",").ToList() ?? new List<string>();
            if (user != null && user.Roles != null) isSuccess = user.Roles.Any(y => roles.Any(role => role == y));
            if (user == null || !isSuccess)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

    }
}