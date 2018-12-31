using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Middleware
{
    using System.Net;
    using System.Net.Http;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    using ServerProject.Models;

    public static class CheckAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }

    public class AuthenticationMiddleware
    {
        private RequestDelegate _nextDelegate;
        public AuthenticationMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task InvokeAsync(HttpContext context , ServerProjectContext projectContext)
        {
            var isValid = false;
            if (context.Session.GetString("currentLogin") != null)
            {

                Accounts accounts = projectContext.Accounts.SingleOrDefault(b => b.UserName == context.Session.GetString("currentLogin"));
                if (accounts != null)
                {
                    isValid = true;
                }
            }
            if (isValid)
            {
                await _nextDelegate(context);
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden");

            }
        }
    }
}
