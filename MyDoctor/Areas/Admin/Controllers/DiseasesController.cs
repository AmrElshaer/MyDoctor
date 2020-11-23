using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;

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


        public IActionResult Index(SearchParamter searchParamter)
        {
            searchParamter.Page = searchParamter.Page ?? 1;
            searchParamter.PageSize = 5;
            var model = _diseasesRepository.GetSearchResult(searchParamter);
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
