using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
namespace MyDoctor.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITableTrackUserRepository _tableTrackUserRepository;
        private readonly IInboxMessageRepsitory _inboxMessageRepsitory;

        public DashBoardController(IInboxMessageRepsitory inboxMessageRepsitory,ICategoryRepository categoryRepository,ITableTrackUserRepository tableTrackUserRepository)
        {
            _categoryRepository = categoryRepository;
            _tableTrackUserRepository = tableTrackUserRepository;
            _inboxMessageRepsitory = inboxMessageRepsitory;


        }
        public async Task<IActionResult>  Index()
        {
           
            var result = await _categoryRepository.GetBoardViewModel(4);
            return View(result);
        }
        public async Task<IActionResult> GeneralSearch(string searchVal)
        {
           
                var result =await _categoryRepository.GeneralSearchAsync(searchVal);
                return PartialView("_GeneralSearchContent",result);
        }
        public async Task UpdateUserTrack() 
        {

            if (User.Identity.IsAuthenticated)
            {
               await  _tableTrackUserRepository.RefreshUserNofificationAsync(User.Identity.Name);
            }
        }
        public async Task UpdateMessages() {
            if (User.Identity.IsAuthenticated)
            {
                await _inboxMessageRepsitory.MakeMessagesSeeAsync(User.Identity.Name);
            }
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

    }
}