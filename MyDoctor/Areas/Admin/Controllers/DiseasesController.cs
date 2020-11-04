using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiseasesController : BaseController
    {
        private readonly IDiseasesRepository _diseasesRepository;
        public DiseasesController(IDiseasesRepository diseasesRepository)
        {
            _diseasesRepository = diseasesRepository;
           
        }


        public IActionResult Index(string query, int? page, DateTime? createFrom, DateTime? createTo)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;
            var model = _diseasesRepository.GetSearchResult(query, pageNumber, pageSize, createFrom, createTo);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(Disease disease)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _diseasesRepository.CreateEdit(disease);
                    AddMessage("Save Disease Is success-ful",isSuccess:true);
                }
                catch (Exception e)
                {
                    AddMessage("Save Disease Is Not success-ful");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _diseasesRepository.DeleteAsync(id);
                AddMessage("Disease Delete Success", isSuccess:true);


            }
            catch (Exception e)
            {
                AddMessage("Disease Delete Failed");


            }
            return RedirectToAction(nameof(Index));
        }
    }
}
