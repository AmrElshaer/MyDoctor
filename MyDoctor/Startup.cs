using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyDoctor.Services;
using MYDoctor.Infrastructure;
using MYDoctor.Infrastructure.Message;
using MYDoctor.Infrastructure.Notification;
using Rotativa.AspNetCore;

namespace MyDoctor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseRequestLocalizationCookies();
            
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(name: "adminarea", 
                    pattern: "{area:exists}/{controller=Diseases}/{action=Index}/{id?}");

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=DashBoard}/{action=Index}/{id?}");
                routes.MapHub<TablesTrackerHup>("/tablesTrackerHup");
                routes.MapHub<MessageHup>("/messageHup");
                routes.MapRazorPages();

            });
            RotativaConfiguration.Setup(env);
        }
    }
}