using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Models;
using MyDoctor.ViewModels;

namespace MyDoctor.Helper
{
    public static class SeedData
    {
        public static  void AddRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!  roleManager.RoleExistsAsync(Roles.Admin.ToString()).GetAwaiter().GetResult() )roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() }).GetAwaiter().GetResult();
            if (! roleManager.RoleExistsAsync(Roles.Client.ToString()).GetAwaiter().GetResult())roleManager.CreateAsync(new IdentityRole { Name = Roles.Client.ToString() }).GetAwaiter().GetResult();
            if (!roleManager.RoleExistsAsync(Roles.Doctor.ToString()).GetAwaiter().GetResult())roleManager.CreateAsync(new IdentityRole { Name = Roles.Doctor.ToString() }).GetAwaiter().GetResult();
            

        }
        
        public  static void  AddUsers(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "Admin@Admin.com";
            var admin = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (admin == null)
            {
                var user = new ApplicationUser {UserName = adminEmail, Email = adminEmail};
                var adduserresuilt = userManager.CreateAsync(user, "Admin123@").GetAwaiter().GetResult();
                if (adduserresuilt.Succeeded) userManager.AddToRoleAsync(user, Roles.Admin.ToString()).GetAwaiter().GetResult();
            }
        }
    }
}
