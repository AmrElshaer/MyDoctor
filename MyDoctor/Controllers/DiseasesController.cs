
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Helper;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    [Authorize]
    public class DiseasesController : Controller
    {
       
        private readonly IDiseasesRepository _diseasesRepository;
             public DiseasesController(IDiseasesRepository diseasesRepository)
        {
            _diseasesRepository = diseasesRepository;
           
        }

        
        public async Task<IActionResult> Index()
        {
            var disease = await _diseasesRepository.GetAll().ToListAsync();
            return View(disease);
        }
      
        // GET: Diseases/Details/5
        public async Task<IActionResult> Details(int id)
        {
         
            var disease = await _diseasesRepository.GetByIdAsync(id);
            if (disease == null) return NotFound();

            return View(disease);
        }

      
    }
}
