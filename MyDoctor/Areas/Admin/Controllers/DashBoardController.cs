using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MYDoctor.Core.Application.IRepository;
using System.Threading.Tasks;

namespace MyDoctor.Areas.Admin.Controllers
{
    [Authorize]
    public class DashBoardController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;

        public DashBoardController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
  
        // GET: DashBoard
        public async Task<ActionResult> Index()
        {
            var category =await  _categoryRepository.GetAdminBoard();
            return View(category);
        }

       
    }
}