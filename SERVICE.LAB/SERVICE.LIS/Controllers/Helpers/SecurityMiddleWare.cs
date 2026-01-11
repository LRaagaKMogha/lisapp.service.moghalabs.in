using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dev.IRepository;
using Service.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DEV.API.SERVICE
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
                httpContext.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
                httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                httpContext.Response.Headers.Add("Content-Security-Policy", "default-src *; style-src 'self' http://* 'unsafe-inline'; script-src 'self' http://* 'unsafe-inline' 'unsafe-eval'; img-src 'self' http://* data:;");
                httpContext.Response.Headers.Add("X-Frame-Options", _config.GetSection("X-Frame-Options").Value);
                //... and so on
                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}