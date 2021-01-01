using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace MyDoctor.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestLocalizationCookiesMiddleware
    {
        private readonly RequestDelegate _next;
        public CookieRequestCultureProvider Provider { get; }
        public RequestLocalizationCookiesMiddleware(RequestDelegate next,IOptions<RequestLocalizationOptions> requestLocalizationOptions)
        {
            Provider =
                requestLocalizationOptions
                    .Value
                    .RequestCultureProviders
                    .Where(x => x is CookieRequestCultureProvider)
                    .Cast<CookieRequestCultureProvider>()
                    .FirstOrDefault();
            _next = next;
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            if (Provider != null)
            {
                var feature = httpContext.Features.Get<IRequestCultureFeature>();

                if (feature != null)
                {
                    // remember culture across request
                    httpContext.Response
                        .Cookies
                        .Append(
                            Provider.CookieName,
                            CookieRequestCultureProvider.MakeCookieValue(feature.RequestCulture)
                        );
                }
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestLocalizationCookiesMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLocalizationCookies(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLocalizationCookiesMiddleware>();
        }
    }
}
