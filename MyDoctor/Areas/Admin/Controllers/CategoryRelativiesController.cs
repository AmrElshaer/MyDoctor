using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class CategoryRelativiesController : BaseController
    {
        private readonly ICategoryRelativiesRepository _categoryRelativiesRepository;

        public CategoryRelativiesController(ICategoryRelativiesRepository categoryRelativiesRepository)=>
            _categoryRelativiesRepository = categoryRelativiesRepository;
        

        public IActionResult Index(string query, int? page, DateTime? createFrom, DateTime? createTo,
            int? beatyandHealthId)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;
            var model = _categoryRelativiesRepository.GetSearchResult(query, pageNumber, pageSize, createFrom,
                createTo, beatyandHealthId);
            return View(model);
        }

        public IActionResult ExportToExcel(string query, int? page, DateTime? createFrom, DateTime? createTo,
            int? beatyandHealthId)
        {
            var relativescategories = _categoryRelativiesRepository.GetAll(
                x =>
                (query == null || x.Address.ToLower().Contains(query.ToLower()))
                && (createFrom == null || x.CreateDate >= createFrom)
                && (createTo == null || x.CreateDate <= createTo)
                && (beatyandHealthId == null || x.BeatyandHealthy.Id == beatyandHealthId.Value),
                rc => rc.OrderByDescending(a => a.Id),
                rc=>rc.BeatyandHealthy
                
                );
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("RelativeCategory");
                var current = 2;
                // workSheetStyle
                WorkSheetExcelStyle(worksheet);
                worksheet.Columns("C").Width = 52;
                worksheet.Columns("D").Width = 52;
                worksheet.Columns("E").Width = 52;
                // Title style
                var titleCell = worksheet.Cell(current, 3);
                TitleExcelStyle(titleCell, "RelativeCategories");
                // Header Style Sheet
                current += 2;
                var numColumn = worksheet.Cell(current, 1);
                var categoryColumn = worksheet.Cell(current, 2);
                var imageColumn = worksheet.Cell(current, 3);
                var address = worksheet.Cell(current, 4);
                var subject = worksheet.Cell(current, 5);
                HeaderExcelStyle(numColumn, "Num");
                HeaderExcelStyle(categoryColumn, "Category");
                HeaderExcelStyle(imageColumn, "ImageUrl");
                HeaderExcelStyle(address, "Address");
                HeaderExcelStyle(subject, "Subject");

                #region body

                var number = 0;
                foreach (var item in relativescategories)
                {
                    current++;
                    var numberrow = worksheet.Cell(current, 1);
                    numberrow.Value = number++;
                    numberrow.Style.Fill.BackgroundColor = XLColor.BabyBlue;
                    var categoryrow = worksheet.Cell(current, 2);
                    categoryrow.Value = item.BeatyandHealthy.Category;
                    var imagerow = worksheet.Cell(current, 3);
                    imagerow.Value = item.ImageOrVideo;
                    imagerow.Hyperlink = new XLHyperlink(item.ImageOrVideo);
                    var addressrow = worksheet.Cell(current, 4);
                    addressrow.Value = item.Address;
                    var subjectrow = worksheet.Cell(current, 5);
                    subjectrow.Value = item.Subject;
                }

                #endregion

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
                    AddMessage("Relative Category Save Success-full", "Message", true);
                }
                catch (Exception)
                {
                    AddMessage("Error When Save Relative Category", "Message");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryRelativiesRepository.DeleteAsync(id);
                AddMessage("Relative Category Delete Success", "Message", true);
            }
            catch (Exception e)
            {
                AddMessage("Relative Category Delete Failed", "Message");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}