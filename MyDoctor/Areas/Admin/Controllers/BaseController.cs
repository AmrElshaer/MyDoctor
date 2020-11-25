using System;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyDoctor.Common;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class BaseController : Controller
    {
        protected void  AddMessage(string content,bool isSuccess=false)
        {
           
            TempData.Put("Message", new Message()
            {
                Content = content,
                IsSuccess = isSuccess
            });
        }
        protected void AddError(ModelStateDictionary modelState)
        {
            TempData.Put("Error",modelState.Values.Where(v=>v.Errors.Any()).Select(a=>new Error {
                ErrorMessages=a.Errors.Select(v=>v.ErrorMessage)}));
        }
        protected void WorkSheetExcelStyle(IXLWorksheet worksheet)
        {
            worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Rows().AdjustToContents();
            worksheet.Columns().AdjustToContents();
            worksheet.Style.Alignment.WrapText = true;
        }

        protected void TitleExcelStyle(IXLCell titleCell,string title)
        {
            titleCell.Value = $"MyDoctoy Report For {title} At {DateTime.Now.ToShortDateString()}";
            titleCell.Style.Fill.BackgroundColor = XLColor.BabyBlue;
            titleCell.RichText.SetFontColor(XLColor.White);
            titleCell.Style.Font.Bold = true;
            titleCell.Style.Font.FontSize = 70;
        }
        protected void HeaderExcelStyle(IXLCell headerCell, string value)
        {
            headerCell.Value = value;
            headerCell.Style.Fill.BackgroundColor = XLColor.BabyBlue;
            headerCell.RichText.SetFontColor(XLColor.White);
            headerCell.Style.Font.Bold = true;
            headerCell.Style.Font.FontSize = 20;
        }
    }
}