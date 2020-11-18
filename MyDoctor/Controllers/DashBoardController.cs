
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;


namespace MyDoctor.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public DashBoardController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult>  Index()
        {
            var result = await _categoryRepository.GetBoardViewModel(4);
            return View(result);
        }
    }
}