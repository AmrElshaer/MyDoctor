using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Infrastructure.File;
using MYDoctor.Infrastructure.Identity;
using MYDoctor.Infrastructure.Notification;
using MYDoctor.Infrastructure.Repository;

namespace MYDoctor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MYDoctor.Infrastructure")));
            //Inject Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();
            //SignalR Config
            services.AddSignalR();
            //Inject Repository
            services.AddScoped<IDoctorRepository,DoctorRepository>();
            services.AddScoped<IDiseasesRepository, DiseasesRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryRelativiesRepository, CategoryRelativiesRepository>();
            services.AddScoped<IMedicinRepository, MedicinRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ITableTrackNotification, TableTrackNotification>();
            services.AddScoped<ITableTrackUserRepository, TableTrackUserRepository>();
            services.AddTransient<IFileConfig, FileConfig>();
            return services;
        }
    }
}
