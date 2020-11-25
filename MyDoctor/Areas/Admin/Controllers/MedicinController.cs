using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class MedicinController : BaseController
    {
        private readonly IMedicinRepository _medicinRepository;
        public MedicinController(IMedicinRepository categoryRepository)
        {
            _medicinRepository = categoryRepository;
        }

        public IActionResult Index(SearchParamter searchParamter)
        {
             searchParamter.Page = searchParamter.Page ?? 1;
            searchParamter.PageSize = 5;
            var model = _medicinRepository.GetSearchResult(searchParamter);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEdit(Medicin medicin)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    if (Request.Form.TryGetValue("Diseases", out var diseasSelect))
                    {
                       diseasSelect.ToList().ForEach(d=> medicin.DiseaseMedicins.Add(new DiseaseMedicin() { DiseaseId =Convert.ToInt32(d)}));
                    }
                    await _medicinRepository.CreateEdit(medicin);
                    AddMessage("Medicin Save Success-full", true);

                }
                catch (Exception)
                {
                    AddMessage("Error When Save Medicin");
                }


            }
            else
            {
                AddError(ModelState);
            }

            return RedirectToAction(nameof(Index));


        }

        

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _medicinRepository.DeleteAsync(id);
                AddMessage("Medicin Delete Success", true);


            }
            catch (Exception e)
            {
                AddMessage("Medicin Delete Failed");


            }
            return RedirectToAction(nameof(Index));
        }


    }
}
