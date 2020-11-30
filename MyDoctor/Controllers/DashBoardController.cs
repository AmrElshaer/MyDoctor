using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Infrastructure.Notification;

namespace MyDoctor.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITableTrackUserRepository _tableTrackUserRepository;
        private readonly ITableTrackNotification _tableTrackNotification;

        public DashBoardController(ICategoryRepository categoryRepository,ITableTrackUserRepository tableTrackUserRepository,ITableTrackNotification tableTrackNotification)
        {
            _categoryRepository = categoryRepository;
            _tableTrackUserRepository = tableTrackUserRepository;
            _tableTrackNotification = tableTrackNotification;
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
    }
}