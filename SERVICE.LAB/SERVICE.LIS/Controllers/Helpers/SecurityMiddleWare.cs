using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Service.API.SERVICE
{
    public class SecurityMiddleWare
    {
        private readonly RequestDelegate _next;
        private IConfiguration _config;

        public SecurityMiddleWare(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;

        }
        public async Task Invoke(HttpContext context)
        {
            //To add Headers AFTER everything you need to do this
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000");
                httpContext.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                httpContext.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
                httpContext.Response.Headers.Append("Content-Security-Policy", "default-src *; style-src 'self' http://* 'unsafe-inline'; script-src 'self' http://* 'unsafe-inline' 'unsafe-eval'; img-src 'self' http://* data:;");
                httpContext.Response.Headers.Append("X-Frame-Options", _config.GetSection("X-Frame-Options").Value);
                //... and so on
                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}