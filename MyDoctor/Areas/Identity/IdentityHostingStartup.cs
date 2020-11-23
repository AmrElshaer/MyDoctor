
using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(MyDoctor.Areas.Identity.IdentityHostingStartup))]
namespace MyDoctor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}