using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Models;
using MyDoctor.ViewModels;

namespace MyDoctor.Controllers
{
    public class MedicinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicinsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Search(string searchparam)
        {
            if (searchparam==null)
            {
                return BadRequest();
            }
            
            var resuilt=   this._context.Medicin.Where(a=>a.name.ToLower().Contains(searchparam.ToLower())).ToList();
            if (resuilt==null)
            {
                return BadRequest();
            }
            return new JsonResult(resuilt);
        }

        // GET: Medicins
        public  IActionResult Index()
        {
        var arr=from p in _context.Medicin
            group p by p.diseasespecific into g
            select new Medicngroup { id = g.Key };
            return  View(arr);
        }

        // GET: Medicins/Details/5
        public async Task<IActionResult> Details(int?  id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicin = await _context.Medicin.FirstOrDefaultAsync(m => m.id==id);
            if (medicin == null)
            {
                return NotFound();
            }

            return View(medicin);
        }
        [HttpGet]
        public IActionResult AllDiseaseRelative(string diseasename)
        {

            if (diseasename == null)
            {
                return NotFound();
            }

            var medicin = _context.Medicin.Where(m => m.diseasespecific == diseasename);
            if (medicin == null)
            {
                return NotFound();
            }

            return View(medicin);
        }
        // GET: Medicins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,price,affects,indications,diseasespecific")] Medicin medicin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicin);
        }

        // GET: Medicins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicin = await _context.Medicin.FindAsync(id);
            if (medicin == null)
            {
                return NotFound();
            }
            return View(medicin);
        }

        // POST: Medicins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,price,affects,indications,diseasespecific")] Medicin medicin)
        {
            if (id != medicin.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinExists(medicin.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicin);
        }

        // GET: Medicins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicin = await _context.Medicin
                .FirstOrDefaultAsync(m => m.id == id);
            if (medicin == null)
            {
                return NotFound();
            }

            return View(medicin);
        }

        // POST: Medicins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicin = await _context.Medicin.FindAsync(id);
            _context.Medicin.Remove(medicin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinExists(int id)
        {
            return _context.Medicin.Any(e => e.id == id);
        }
    }
}
