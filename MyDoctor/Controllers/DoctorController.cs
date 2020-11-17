using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyDoctor.IRepository;
using MyDoctor.Models;
using MyDoctor.ViewModels;

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
          
               var doctors =await _doctorRepository.GetAll(
                d => (!doctorSearch.Categories.Any()|| doctorSearch.Categories.Contains(d.CategoryId))
                && (!doctorSearch.Countries.Any() || doctorSearch.Countries.Contains(d.Country))
                && (!doctorSearch.Cities.Any() || doctorSearch.Cities.Contains(d.City))
                && ( string.IsNullOrEmpty(doctorSearch.Name) || d.Name.ToLower().Contains(doctorSearch.Name.ToLower()))
                 , d => d.OrderByDescending(o => o.Id), d => d.Category).ToListAsync();
                return View(new DoctorViewModel(doctors,doctorSearch));
            
         
            
        }

        public async Task<IActionResult> Profile(int id) {
            var doctor = await _doctorRepository.GetByIdAsync(id, d => d.Category);
            return View(doctor);
        }

        

      
    }
}