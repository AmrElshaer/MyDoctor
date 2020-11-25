﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MYDoctor.Infrastructure.Identity;
using MYDoctor.Core.Application.Common.Enum;
using MYDoctor.Infrastructure.File;
using System.Linq;

namespace MyDoctor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFileConfig _fileConfig;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterModel(
           IFileConfig fileConfig,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
           )
        {
            _fileConfig = fileConfig;
            _userManager = userManager;
            _signInManager = signInManager;
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

                string uniquename = null;
                if (Input.ImagePath!=null)
                        _fileConfig.AddFile(Input.ImagePath, "images");
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, ImagePath = uniquename };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(Input.Email), nameof(Roles.Client));
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                else { 
                      foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
              
            }
            else {
                ModelState.Values.Where(v => v.Errors.Any()).ToList().ForEach(a =>
                {
                    a.Errors.ToList().ForEach(err =>
                    {
                        ModelState.AddModelError(string.Empty, err.ErrorMessage);
                    });

                });
                
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
