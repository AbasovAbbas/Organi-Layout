using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroSignalR.appCode.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //var error = httpContext.Features.Get<IExceptionHandlerFeature>();
                var feature = httpContext.Features.Get<IEndpointFeature>();
                var endpoints = feature?.Endpoint;
                var cad = endpoints.Metadata.OfType<ControllerActionDescriptor>().FirstOrDefault();
                if (cad != null)
                {
                    Log.Fatal($"Controller : {cad.ControllerName}; Action : {cad.ActionName};Message: {ex.Message}");
                }
                else
                {
                    Log.Fatal(ex.Message);
                }
                httpContext.Response.Redirect("/error.html");
            }
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}
