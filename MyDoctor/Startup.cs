using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MYDoctor.Infrastructure;
using MYDoctor.Infrastructure.Message;
using MYDoctor.Infrastructure.Notification;

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
        public  void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
             
            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<TablesTrackerHup>("/tablesTrackerHup");
                routes.MapHub<MessageHup>("/messageHup");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "adminarea", template: "{area:exists}/{controller=Diseases}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=DashBoard}/{action=Index}/{id?}");
               
                
            });
        }
    }
}
