using ClosedXML.Excel;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Application.IHelper
{
    public interface IExcelHelper
    {
        void AddCategoryExcelConfig(IXLWorksheet worksheet, IEnumerable<BeatyandHealthy> categories);
        void AddRelativesCategoryExcelConfig(IXLWorksheet worksheet, IEnumerable<RelativeofBeatyandhealthy> relativesCategories);
    }
}
