using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class RelativeDiseasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelativeDiseasesController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        // GET: RelativeDiseases
        public async Task<IActionResult> Index(string DiseaseName)
        {
            return Json(await _context.RelativeDisease.Where(a => a.DiseaseName == DiseaseName).ToListAsync());
        }

        // GET: RelativeDiseases/Details/5
        

        // GET: RelativeDiseases/Create
        public IActionResult Create()
        {
            TempData["AllDisease"] = _context.Disease.Select(a => a.DiseaseName).ToList();
            return View();
        }

        // POST: RelativeDiseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiseaseName,ImageOrvideo,subject,DiseaseAddress")] RelativeDisease relativeDisease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relativeDisease);
                await _context.SaveChangesAsync();
               return Redirect("/diseases/index");
            }
            return View(relativeDisease);
        }

       
       
    }
}
