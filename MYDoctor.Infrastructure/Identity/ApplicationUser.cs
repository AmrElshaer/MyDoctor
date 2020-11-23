using Microsoft.AspNetCore.Identity;
namespace MYDoctor.Infrastructure.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string ImagePath { get; set; }

    }
}
