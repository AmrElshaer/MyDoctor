using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.IRepository;

namespace MyDoctor.Controllers
{
    public class RelativesCategoryController : Controller
    {
        private readonly ICategoryRelativiesRepository _relativeCategoryRepository;

        public RelativesCategoryController(ICategoryRelativiesRepository categoryRelativiesRepository)
        {
            _relativeCategoryRepository = categoryRelativiesRepository;
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var beatyandHealthy = await _relativeCategoryRepository.GetRelativeCategoryAsync(id.Value, 4);
            if (beatyandHealthy == null) return NotFound();
            return View(beatyandHealthy);
        }
    }
}