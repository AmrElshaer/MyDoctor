using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
namespace MyDoctor.Controllers
{
    public class BeatyandHealthiesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public BeatyandHealthiesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            
        }


        public async Task<IActionResult> Details(int id)
        {
            
            var beatyandHealthy = await _categoryRepository.GetCategoryAsync(id,4);
            if (beatyandHealthy == null) return NotFound();
            return View(beatyandHealthy);
        }

    }
}