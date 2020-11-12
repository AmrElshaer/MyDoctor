
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.IRepository;
using MyDoctor.ViewModels;

namespace MyDoctor.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMedicinRepository _medicinRepository;
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly ICategoryRelativiesRepository _categoryRelativiesRepository;
        public DashBoardController(ICategoryRepository categoryRepository, IDoctorRepository doctorRepository, IMedicinRepository medicinRepository, IDiseasesRepository diseasesRepository, ICategoryRelativiesRepository categoryRelativiesRepository)
        {
            _categoryRepository = categoryRepository;
            _doctorRepository = doctorRepository;
            _medicinRepository = medicinRepository;
            _diseasesRepository = diseasesRepository;
            _categoryRelativiesRepository = categoryRelativiesRepository;
        }
        public async Task<IActionResult>  Index()
        {
            var result=new DashBoardViewModel()
            {
                Categories =await _categoryRepository.Search().Take(4).ToListAsync(),
                Doctors = await _doctorRepository.Search().Take(4).ToListAsync(),
                Medicins = await _medicinRepository.Search().Take(4).ToListAsync(),
                Diseases = await _diseasesRepository.Search().Take(4).ToListAsync(),
                RelativeCategories = await _categoryRelativiesRepository.Search().Take(4).ToListAsync()
            };
            return View(result);
        }
    }
}