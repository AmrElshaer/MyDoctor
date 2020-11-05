using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class MedicinController : BaseController
    {
        private readonly IMedicinRepository _medicinRepository;
        public MedicinController(IMedicinRepository categoryRepository)
        {
            _medicinRepository = categoryRepository;
        }

        public IActionResult Index(string query, int? page, DateTime? createFrom, DateTime? createTo)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;
            var model = _medicinRepository.GetSearchResult(query, pageNumber, pageSize, createFrom, createTo);

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
                    AddMessage("Medicin Save Success-full", "Message", true);

                }
                catch (Exception)
                {
                    AddMessage("Error When Save Medicin", "Message");
                }


            }

            return RedirectToAction(nameof(Index));


        }

        

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _medicinRepository.DeleteAsync(id);
                AddMessage("Medicin Delete Success", "Message", true);


            }
            catch (Exception e)
            {
                AddMessage("Medicin Delete Failed", "Message");


            }
            return RedirectToAction(nameof(Index));
        }


    }
}
