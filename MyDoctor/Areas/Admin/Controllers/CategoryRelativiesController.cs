using System;
using System.IO;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Services;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using Rotativa.AspNetCore;
using static MyDoctor.Services.RenderRazorService;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class CategoryRelativiesController : BaseController
    {
        private readonly ICategoryRelativiesRepository _categoryRelativiesRepository;
        private readonly IExcelHelper _excelHelper;

        public CategoryRelativiesController(ICategoryRelativiesRepository categoryRelativiesRepository,IExcelHelper excelHelper) { 
        
         _categoryRelativiesRepository = categoryRelativiesRepository;
            _excelHelper = excelHelper;
        }
           
        

        public IActionResult Index(SearchParamter searchParamter)
        {
            searchParamter.Page = searchParamter.Page ?? 1;
            searchParamter.PageSize = 5;
            var model = _categoryRelativiesRepository.GetSearchResult(searchParamter);
            return View(model);
        }

        public async Task<IActionResult> ExportToExcel(SearchParamter searchParamter)
        {
            var relativescategories =await _categoryRelativiesRepository.SearchHits(searchParamter).ToListAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("RelativeCategory");
                _excelHelper.AddRelativesCategoryExcelConfig(worksheet,relativescategories);
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(content, contentType);
                }
            }
        }
        public async Task<IActionResult> ExportToPDF(SearchParamter searchParamter)
        {
            var categories = await _categoryRelativiesRepository.SearchHits(searchParamter).ToListAsync();
            return new ViewAsPdf(categories);

        }
        [NoDirectAccess]
        public async Task<IActionResult> CreateEdit(int id = 0)
        {
            if (id == 0)
                return View("_CreateEdit", new RelativeofBeatyandhealthy());
            else
            {
                var categoryRelative = await _categoryRelativiesRepository.GetByIdAsync(id);
                if (categoryRelative == null)
                {
                    return NotFound();
                }
                return View("_CreateEdit", categoryRelative);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateEdit(RelativeofBeatyandhealthy relativeCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRelativiesRepository.CreateEdit(relativeCategory);
                }
                catch (Exception)
                {
                    throw;
                }
                return Json(new
                {
                    isValid = true,
                    html = RenderRazorService.RenderRazorViewToString(this, "_ViewAll",
                   _categoryRelativiesRepository.GetSearchResult(new SearchParamter() { PageSize = 5, Page = 1 }))
                });
            }
            return Json(new { isValid = false, html = RenderRazorService.RenderRazorViewToString(this, "_CreateEdit", relativeCategory) });
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryRelativiesRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(new
            {
                isValid = true,
                html = RenderRazorService.RenderRazorViewToString(this, "_ViewAll",
                              _categoryRelativiesRepository.GetSearchResult(new SearchParamter() { PageSize = 5, Page = 1 }))
            });
        }
    }
}