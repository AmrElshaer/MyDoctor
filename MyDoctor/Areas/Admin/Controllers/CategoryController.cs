using System;
using System.IO;
using System.Linq;
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
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> CreateEdit(BeatyandHealthy healthBeatyandHealthy)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.CreateEdit(healthBeatyandHealthy);
                    AddMessage("Category Save Success-full", isSuccess: true);

                }
                catch (Exception)
                {
                    AddMessage("Error When Save Category");
                }


            }
            else {
                AddError(ModelState);
            }
            
            return RedirectToAction(nameof(Index));
           
            
        }

        public async Task<IActionResult> ExportToExcel(SearchParamter searchParamter)
        {
            var categories =await _categoryRepository.GetSearchHits(searchParamter).ToListAsync();
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

        public async Task<ActionResult>  Delete(int id)
        {
            try
            {  
                 await _categoryRepository.DeleteAsync(id);
                 AddMessage("Category Delete Success",true);
                

            }
            catch (Exception e)
            {
                AddMessage("Category Delete Failed");

                
            }
            return RedirectToAction(nameof(Index));
        }

       
    }
}