using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.IRepository;

namespace MyDoctor.Controllers
{
    public class BeatyandHealthiesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public BeatyandHealthiesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beatyandHealthy = await _categoryRepository.GetCategoryAsync(id.Value,4);
            if (beatyandHealthy == null) return NotFound();
            return View(beatyandHealthy);
        }

    }
}