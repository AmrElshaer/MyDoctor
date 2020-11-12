using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
using MyDoctor.ISerivce;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class BeatyandHealthiesController : Controller
    {
        private readonly ICategorySerivce _categorySerivce;

        public BeatyandHealthiesController(ICategorySerivce categorySerivce)
        {
            _categorySerivce = categorySerivce;
            
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var beatyandHealthy = await _categorySerivce.GetCategory(id.Value);
            if (beatyandHealthy == null) return NotFound();
            return View(beatyandHealthy);
        }

    }
}