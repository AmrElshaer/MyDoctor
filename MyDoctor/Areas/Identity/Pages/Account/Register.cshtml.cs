
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyDoctor.Models;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MyDoctor.Helper;

namespace MyDoctor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<CutomPropertiy> _signInManager;
        private readonly IHostingEnvironment _ihostEnv;
        private readonly UserManager<CutomPropertiy> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
           IHostingEnvironment hostingEnvironment,
            UserManager<CutomPropertiy> userManager,
            SignInManager<CutomPropertiy> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _ihostEnv = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public CustomResigter Input { get; set; }

        public string ReturnUrl { get; set; }

       
        public class CustomResigter
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            public IFormFile ImagePath { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)

            {
                string uniquename = RegisterHelper.ConfigImagePath(Input.ImagePath,_ihostEnv);
                var user = new CutomPropertiy { UserName = Input.Email, Email = Input.Email, ImagePath=uniquename };
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await  _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(Input.Email),"Client");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
