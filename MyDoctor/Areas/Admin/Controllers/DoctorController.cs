using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IDoctorRepository _doctorRepository;
       

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IActionResult Index(string query, int? page,int? beatyandHealthId)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;
            var model = _doctorRepository.GetSearchResult(query, pageNumber, pageSize,beatyandHealthId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEdit(MyDoctor.Models.Doctor doctor,IFormFile image)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    await _doctorRepository.CreateEdit(doctor,image);
                    AddMessage("Doctor Save Success-full", "Message", true);

                }
                catch (Exception)
                {
                    
                    AddMessage("Error When Save Doctor", "Message");

                }


            }

            return RedirectToAction(nameof(Index));


        }

      

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _doctorRepository.DeleteDoctorAsync(id);
                
                AddMessage("Doctor Delete Success", "Message", true);


            }
            catch (Exception e)
            {
                AddMessage("Doctor Delete Failed", "Message");


            }
            return RedirectToAction(nameof(Index));
        }
    }
}