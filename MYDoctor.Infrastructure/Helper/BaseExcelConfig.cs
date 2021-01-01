using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Infrastructure.Helper
{
    public abstract class BaseExcelConfig
    {
        public void HeaderExcelStyle(IXLCell headerCell, string value)
        {
            headerCell.Value = value;
            headerCell.Style.Fill.BackgroundColor = XLColor.BabyBlue;
            headerCell.RichText.SetFontColor(XLColor.White);
            headerCell.Style.Font.Bold = true;
            headerCell.Style.Font.FontSize = 20;
        }

        public void TitleExcelStyle(IXLCell titleCell, string title)
        {
            titleCell.Value = $"MyDoctoy Report For {title} At {DateTime.Now.ToShortDateString()}";
            titleCell.Style.Fill.BackgroundColor = XLColor.BabyBlue;
            titleCell.RichText.SetFontColor(XLColor.White);
            titleCell.Style.Font.Bold = true;
            titleCell.Style.Font.FontSize = 70;
        }

        public void WorkSheetExcelStyle(IXLWorksheet worksheet)
        {
            worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Rows().AdjustToContents();
            worksheet.Columns().AdjustToContents();
            worksheet.Style.Alignment.WrapText = true;
        }
    }
}
