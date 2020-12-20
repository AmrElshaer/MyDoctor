using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.IRepository;

namespace MyDoctor.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        public async Task<IActionResult> Profile()
        {
            var userEmail = User.Identity.Name;
            var user = await _userProfileRepository.GetUserProfileAsync(userEmail);
            return View(user);
        }
    }
}