
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using Newtonsoft.Json;

namespace MyDoctor.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public DashBoardController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult>  Index()
        {
            var result = await _categoryRepository.GetBoardViewModel(4);
            return View(result);
        }
        public async Task<IActionResult> GeneralSearch(string searchVal)
        {
                var result =await _categoryRepository.GeneralSearchAsync(searchVal);
                return PartialView("_GeneralSearchContent",result);
        }
    }
}