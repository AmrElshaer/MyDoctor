using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Services;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using NToastNotify;
using Rotativa.AspNetCore;
using static MyDoctor.Services.RenderRazorService;

namespace MyDoctor.Areas.Admin.Controllers
{
   
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IExcelHelper _excelHelper;

        public CategoryController(ICategoryRepository categoryRepository,IExcelHelper excelHelper)
        {
            _categoryRepository = categoryRepository;
            _excelHelper = excelHelper;
        }
      
        public IActionResult Index(SearchParamter searchParamter)
        {
            searchParamter.Page=searchParamter.Page??1;
            searchParamter.PageSize = 5;
            var model = _categoryRepository.GetSearchResult(searchParamter);
            
            return View(model);
        }
        [NoDirectAccess]
        public async Task<IActionResult> CreateEdit(int id = 0)
        {
            if (id == 0)
                return View("_CreateEdit", new BeatyandHealthy());
            else
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View("_CreateEdit",category);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> CreateEdit(BeatyandHealthy healthBeatyandHealthy)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.CreateEdit(healthBeatyandHealthy);
                }
                catch (Exception)
                {
                   throw;
                }
                return Json(new { isValid = true, html = RenderRazorService.RenderRazorViewToString(this, "_ViewAll",
                    _categoryRepository.GetSearchResult(new SearchParamter() { PageSize=5,Page=1})) });
            }
            return Json(new { isValid = false, html = RenderRazorService.RenderRazorViewToString(this, "_CreateEdit", healthBeatyandHealthy) });


        }

        public async Task<IActionResult> ExportToExcel(SearchParamter searchParamter)
        {
            var categories =await _categoryRepository.GetSearchHits(searchParamter);
            using (var workbook=new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Category");
                _excelHelper.AddCategoryExcelConfig(worksheet,categories);
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(content, contentType,fileDownloadName:"Categories.xlsx");
                }
                
            }
        }
        //export to pdf
        public async Task<IActionResult> ExportToPDF(SearchParamter searchParamter)
        {
            var categories =await  _categoryRepository.GetSearchHits(searchParamter);
            return new ViewAsPdf(categories);
           
        }
        public async Task<ActionResult>  Delete(int id)
        {
            try
            {  
                 await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(new
            {
                isValid = true,
                html = RenderRazorService.RenderRazorViewToString(this, "_ViewAll",
                     _categoryRepository.GetSearchResult(new SearchParamter() { PageSize = 5, Page = 1 }))
            });
        }

       
    }
}
