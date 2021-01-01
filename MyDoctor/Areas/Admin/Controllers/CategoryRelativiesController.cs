using System;
using System.IO;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;

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

        public async Task<IActionResult> CreateEdit(RelativeofBeatyandhealthy relativeCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRelativiesRepository.CreateEdit(relativeCategory);
                    AddMessage("Relative Category Save Success-full", true);
                }
                catch (Exception)
                {
                    AddMessage("Error When Save Relative Category");
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
                await _categoryRelativiesRepository.DeleteAsync(id);
                AddMessage("Relative Category Delete Success", true);
            }
            catch (Exception)
            {
                AddMessage("Relative Category Delete Failed");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}