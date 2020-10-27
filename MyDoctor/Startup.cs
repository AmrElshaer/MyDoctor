using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDoctor.Models;
using Microsoft.AspNetCore.Http.Features;
using MyDoctor.Helper;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyDoctor.Areas.Identity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyDoctor.IRepository;
using MyDoctor.Repository;

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
            
            services.Configure<CookiePolicyOptions>(options =>
            { 
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.Configure<FormOptions>(x =>
            //{
            //    x.ValueLengthLimit = int.MaxValue;
            //    x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            //});
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<CutomPropertiy, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();
            
             services.ConfigureApplicationCookie(o => o.LoginPath = "/identity/Account/Login");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Dependency Services
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();
            services.AddScoped<ICommentRepository,CommentRepository>();
            services.AddScoped<IDiseaseRelativeRepository,DiseaseRelativeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IHostingEnvironment env,UserManager<CutomPropertiy> userManager,RoleManager<IdentityRole>roleManager)
        {
             SeedData.AddRoles(roleManager);
             SeedData.AddUsers(userManager);
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=diseases}/{action=Index}/{id?}");
            });
        }
    }
}
