using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEV.API.SERVICE
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IJWTManagerRepository jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var MenuCode = context.Request.Headers["MenuCode"].FirstOrDefault();
            var userData = jwtUtils.ValidateToken(token ?? "");
            if (userData != null)
            {
                context.Items["User"] = userData;
                var isSame = QueryStringChanges(context, userData);
                var issession = jwtUtils.ValidateSession(token);
                //var IsmenuAccess = jwtUtils.ValidateMenu(token, MenuCode);
                if (!isSame || !issession) //|| !IsmenuAccess
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    var problemDetails = new ProblemDetails
                    {
                        Title = "UnAuthorized",
                        Detail = "The requested action is not allowed as the details in Querystring and token doesnot match.",
                        Status = StatusCodes.Status401Unauthorized // Set the appropriate HTTP status code
                    };
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
                    return; // Terminate the middleware pipeline
                }
            }
            await _next(context);
        }

        private bool QueryStringChanges(HttpContext context, UserClaimsIdentity userData)
        {
            var queryString = context.Request.QueryString.Value;
            var queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString);

            bool venueNoSame = UpdateQueryParameter(queryDict, "VenueNo", userData.VenueNo);
            //bool venueBranchNoSame = UpdateQueryParameter(queryDict, "VenueBranchNo", userData.VenueBranchNo);
            bool usernoSame = UpdateQueryParameter(queryDict, "userno", userData.UserNo);

            var newQueryString = QueryString.Create(queryDict);
            context.Request.QueryString = newQueryString;

            return venueNoSame && usernoSame; //&& venueBranchNoSame 
        }

        private bool UpdateQueryParameter(Dictionary<string, Microsoft.Extensions.Primitives.StringValues> queryDict, string key, int value)
        {
            if (!queryDict.ContainsKey(key)) return true;
            bool isSame = queryDict.TryGetValue(key, out var existingValue) && existingValue == value.ToString();
            queryDict[key] = new[] { value.ToString() };
            return isSame;
        }
    }
}