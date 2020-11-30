using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.Common.Search;
using Entities= MYDoctor.Core.Domain.Entities;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Infrastructure.File;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IFileConfig _fileConfig;

        public DoctorController(IDoctorRepository doctorRepository,IFileConfig fileConfig)
        {
            _doctorRepository = doctorRepository;
            _fileConfig = fileConfig;
        }

        public IActionResult Index(SearchParamter searchParamter)
        {
            searchParamter.Page = searchParamter.Page ?? 1;
            searchParamter.PageSize = 5;
            var model = _doctorRepository.GetSearchResult(searchParamter);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEdit(Entities.Doctor doctor,IFormFile image)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                        doctor.ImagePath = _fileConfig.AddFile(image, "images");
                    await _doctorRepository.CreateEdit(doctor);
                    AddMessage("Doctor Save Success-full", true);
                }
                catch (Exception e)
                {

                    AddMessage("Error When Save Doctor");

                }
            }
            else {
                AddError(ModelState);
            }

            return RedirectToAction(nameof(Index));


        }

      

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                
                await _doctorRepository.DeleteDoctorAsync(id);
                
                AddMessage("Doctor Delete Success", true);


            }
            catch (Exception e)
            {
                AddMessage("Doctor Delete Failed");


            }
            return RedirectToAction(nameof(Index));
        }
    }
}