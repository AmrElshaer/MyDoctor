﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyDoctor.Services;
using MYDoctor.Infrastructure.Identity;
using FluentValidation.AspNetCore;
using MYDoctor.Infrastructure.Validation;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace MyDoctor
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryValidator>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.Configure<RequestLocalizationOptions>(options => {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar"),
                   
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.FallBackToParentUICultures = true;
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Remove(new AcceptLanguageHeaderRequestCultureProvider());
            });
            //Inject Claim Factory
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, DoctorClaimsPrincipalFactory>();
            services.AddMemoryCache();

            return services;
        }
    }
}
