using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


    }
}