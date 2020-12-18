using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;

namespace MyDoctor.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }


        public async Task<IActionResult> Index(DoctorSearch doctorSearch)
        {
          
               var doctors =await _doctorRepository.GetDoctorsAsync(doctorSearch);
                return View(new DoctorViewModel(doctors,doctorSearch));
            
         
            
        }

        public async Task<IActionResult> Profile(int id) {
            var doctor = await _doctorRepository.DoctorProfileAsync(id);
            return View(doctor);
        }

        

      
    }
}