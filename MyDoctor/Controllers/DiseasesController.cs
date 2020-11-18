
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Helper;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    
    public class DiseasesController : Controller
    {
       
        private readonly IDiseasesRepository _diseasesRepository;
        public DiseasesController(IDiseasesRepository diseasesRepository)
        {
            _diseasesRepository = diseasesRepository;
           
        }

        
        public async Task<IActionResult> Index(string diseaseName)
        {
            var disease = await _diseasesRepository.GetAll(m=>string.IsNullOrEmpty(diseaseName)||m.DiseaseName.ToLower().Contains(diseaseName.ToLower())
            ,m=>m.OrderByDescending(a=>a.Id),
            d=>d.BeatyandHealthy).ToListAsync();
            return View((disease,diseaseName));
        }
      
        // GET: Diseases/Details/5
        public async Task<IActionResult> Details(int id)
        {
         
            var disease = await _diseasesRepository.GetDiseaseAsync(id,4);
            if (disease == null) return NotFound();

            return View(disease);
        }

      
    }
}
