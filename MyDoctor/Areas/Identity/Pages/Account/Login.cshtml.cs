using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MYDoctor.Infrastructure.Identity;
using MYDoctor.Core.Application.Common.Enum;
using MYDoctor.Infrastructure.Repository;
using MYDoctor.Core.Application.IRepository;

namespace MyDoctor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepository _doctorRepository;

        public LoginModel(IDoctorRepository doctorRepository,ApplicationDbContext context,SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger,UserManager<ApplicationUser>userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _doctorRepository = doctorRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe,lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    
                    var user = await _userManager.FindByNameAsync(Input.Email);
                    var roles= await _userManager.GetRolesAsync(user);
                    switch (roles.FirstOrDefault())
                    {
                        
                        
                        case nameof(Roles.Admin):
                            return RedirectToAction("Index", "DashBoard", new { area = nameof(Roles.Admin) });

                        case nameof(Roles.Doctor):
                            var doctor = await _doctorRepository.GetFirstAsync(a=>a.Email==user.Email);
                            return RedirectToAction("Profile", "Doctor", new { area = string.Empty,id=doctor.Id});
                        default:
                            return RedirectToAction("Index", "DashBoard", new { area = string.Empty });


                    }
                }
                if (result.RequiresTwoFactor)
                     return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
               
                if (result.IsLockedOut)
                     return RedirectToPage("./Lockout");
               
                ModelState.AddModelError(string.Empty,"Email Or Password Is Not Correct");
               
            }
            
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
