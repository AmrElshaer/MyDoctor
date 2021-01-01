using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Infrastructure.File;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Infrastructure.Identity;
using MYDoctor.Infrastructure.Repository;
using MYDoctor.Infrastructure.Validation;

namespace MYDoctor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MYDoctor.Infrastructure")));
            //Inject Identity
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.ConfigureApplicationCookie(o => {
                o.LoginPath = new Microsoft.AspNetCore.Http.PathString("/identity/Account/Login");
            });
           
            //Inject Repository
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IInboxMessageRepsitory, InboxMessageRepsitory>();
            services.AddScoped<IDoctorRepository,DoctorRepository>();
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryRelativiesRepository, CategoryRelativiesRepository>();
            services.AddScoped<IMedicinRepository, MedicinRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ITableTrackNotification, TableTrackNotification>();
            services.AddScoped<ITableTrackUserRepository, TableTrackUserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IDisLikeRepository, DisLikeRepository>();
            services.AddTransient<IFileConfig, FileConfig>();
            //Inject Helper
            services.AddTransient<IDoctorHelper, DoctorHelper>();
            services.AddTransient<IDiseaseHelper,DiseaseHelper>();
            services.AddTransient<IPostHelper, PostHelper>();
            services.AddTransient<IMedicinHelper, MedicinHelper>();
            services.AddTransient<IRelativeCategoryHelper, RelativeCategoryHelper>();
            services.AddSingleton<IValidatorResource, ValidatorResource>();
            services.AddSingleton<IExcelHelper, ExcelHelper>();
            //SignalR Config
            services.AddSignalR();
            return services;
        }
    }
}
