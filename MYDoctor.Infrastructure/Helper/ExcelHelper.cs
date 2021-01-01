using ClosedXML.Excel;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Infrastructure.Helper
{
    public class ExcelHelper : BaseExcelConfig, IExcelHelper
    {
        #region Category Excel Config
        void AddCategoryBody(IXLWorksheet worksheet, IEnumerable<BeatyandHealthy> categories, int startRow)
        {
            var number = 0;
            foreach (var item in categories)
            {
                startRow++;
                var numberrow = worksheet.Cell(startRow, 1);
                numberrow.Value = number++;
                numberrow.Style.Fill.BackgroundColor = XLColor.BabyBlue;
                var categoryrow = worksheet.Cell(startRow, 2);
                categoryrow.Value = item.Category;
                var imagerow = worksheet.Cell(startRow, 3);
                imagerow.Value = item.Image;
                imagerow.Hyperlink = new XLHyperlink(item.Image);
            }
        }

        public void AddCategoryExcelConfig(IXLWorksheet worksheet, IEnumerable<BeatyandHealthy> categories)
        {
            worksheet.Columns("C").Width = 52;
            var current = 2;
            // Title style
            var titleCell = worksheet.Cell(current, 3);
           TitleExcelStyle(titleCell, "Categories");
            // Header Style Sheet
            current += 2;
            AddCategoryHeader(worksheet, current);
            //add body
           AddCategoryBody(worksheet, categories, current);
            // workSheetStyle
           WorkSheetExcelStyle(worksheet);
        }

         void AddCategoryHeader(IXLWorksheet worksheet, int startRow)
        {
            var numColumn = worksheet.Cell(startRow, 1);
            var categoryColumn = worksheet.Cell(startRow, 2);
            var imageColumn = worksheet.Cell(startRow, 3);
            HeaderExcelStyle(numColumn, "Num");
            HeaderExcelStyle(categoryColumn, "Category");
            HeaderExcelStyle(imageColumn, "ImageUrl");
        }


        #endregion
        #region Relative Category Excel Config
        public void AddRelativesCategoryExcelConfig(IXLWorksheet worksheet, IEnumerable<RelativeofBeatyandhealthy> categories)
        {
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
            AddRelativesCategoryHeader(worksheet, current);
            //add body
            AddRelativesCategoryBody(worksheet, categories, current);
            // workSheetStyle
            WorkSheetExcelStyle(worksheet);

        }
        void AddRelativesCategoryHeader(IXLWorksheet worksheet, int startRow)
        {
            startRow += 2;
            var numColumn = worksheet.Cell(startRow, 1);
            var categoryColumn = worksheet.Cell(startRow, 2);
            var imageColumn = worksheet.Cell(startRow, 3);
            var address = worksheet.Cell(startRow, 4);
            var subject = worksheet.Cell(startRow, 5);
            HeaderExcelStyle(numColumn, "Num");
            HeaderExcelStyle(categoryColumn, "Category");
            HeaderExcelStyle(imageColumn, "ImageUrl");
            HeaderExcelStyle(address, "Address");
            HeaderExcelStyle(subject, "Subject");

        }
        void AddRelativesCategoryBody(IXLWorksheet worksheet, IEnumerable<RelativeofBeatyandhealthy> relativesCategories, int startRow)
        {
            var number = 0;
            foreach (var item in relativesCategories)
            {
                startRow++;
                var numberrow = worksheet.Cell(startRow, 1);
                numberrow.Value = number++;
                numberrow.Style.Fill.BackgroundColor = XLColor.BabyBlue;
                var categoryrow = worksheet.Cell(startRow, 2);
                categoryrow.Value = item.BeatyandHealthy.Category;
                var imagerow = worksheet.Cell(startRow, 3);
                imagerow.Value = item.ImageOrVideo;
                imagerow.Hyperlink = new XLHyperlink(item.ImageOrVideo);
                var addressrow = worksheet.Cell(startRow, 4);
                addressrow.Value = item.Address;
                var subjectrow = worksheet.Cell(startRow, 5);
                subjectrow.Value = item.Subject;
            }
        }
        #endregion

    }
}
