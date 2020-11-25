﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;

namespace MyDoctor.Areas.Admin.Controllers
{
   
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
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
            var categories =await _categoryRepository.GetAll(x =>
                (searchParamter.SearchQuery == null || x.Category.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
                && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo)
                , c => c.OrderByDescending(a => a.Id)
                ).ToListAsync();
            using (var workbook=new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Category");
                var current = 2;
                // workSheetStyle
                WorkSheetExcelStyle(worksheet);
                worksheet.Columns("C").Width = 52;
                // Title style
                var titleCell = worksheet.Cell(current, 3);
                TitleExcelStyle(titleCell, "Categories");
                // Header Style Sheet
                current += 2;
                var numColumn = worksheet.Cell(current, 1);
                var categoryColumn = worksheet.Cell(current, 2);
                var imageColumn = worksheet.Cell(current, 3);
                HeaderExcelStyle(numColumn, "Num");
                HeaderExcelStyle(categoryColumn, "Category");
                HeaderExcelStyle(imageColumn, "ImageUrl");

                #region body

                var number = 0;
                foreach (var item in categories)
                {
                    current++;
                    var numberrow = worksheet.Cell(current, 1);
                    numberrow.Value = number++;
                    numberrow.Style.Fill.BackgroundColor = XLColor.BabyBlue;
                    var categoryrow = worksheet.Cell(current, 2);
                    categoryrow.Value = item.Category;
                    var imagerow = worksheet.Cell(current, 3);
                    imagerow.Value = item.Image;
                    imagerow.Hyperlink = new XLHyperlink(item.Image);
                }

                #endregion

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