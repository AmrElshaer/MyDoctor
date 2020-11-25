using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyDoctor.Services;
using MYDoctor.Infrastructure.Identity;
using FluentValidation.AspNetCore;
using MYDoctor.Infrastructure.Validation;

namespace MyDoctor
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.ConfigureApplicationCookie(o => o.LoginPath = "/identity/Account/Login");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryValidator>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Inject Claim Factory
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, DoctorClaimsPrincipalFactory>();
            return services;
        }
    }
}
