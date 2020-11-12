
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDoctor.Models;
using MyDoctor.Helper;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyDoctor.Areas.Identity.Services;
using MyDoctor.IRepository;
using MyDoctor.ISerivce;
using MyDoctor.Repository;
using MyDoctor.Serivce;

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
           
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();
            //Inject Claim Factory
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, DoctorClaimsPrincipalFactory>();
            services.ConfigureApplicationCookie(o => o.LoginPath = "/identity/Account/Login");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Dependency Serivce
            services.AddTransient<IDashBoardSerivce, DashBoardSerivce>();
            services.AddTransient<ICategorySerivce,CategorySerivce>();
            //Dependency Repository
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();
            services.AddScoped<ICommentRepository,CommentRepository>();
            services.AddScoped<IDiseaseRelativeRepository,DiseaseRelativeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentForDoctorPostRepository,CommentForDoctorPostRepository>();
            services.AddScoped<ICategoryRelativiesRepository, CategoryRelativiesRepository>();
            services.AddScoped<IMedicinRepository,MedicinRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IHostingEnvironment env,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole>roleManager)
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
                routes.MapRoute(name: "adminarea", template: "{area:exists}/{controller=Diseases}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=DashBoard}/{action=Index}/{id?}");
               
                
            });
        }
    }
}
