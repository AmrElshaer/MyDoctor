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
    public class updateRelativeBeatyandHealthiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public updateRelativeBeatyandHealthiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: updateRelativeBeatyandHealthies
        public IActionResult Index(int id)
        {
            var data = _context.updateRelativeBeatyandHealthy.Where(a => a.BeatyId == id);
            return Json(data);
        }

        // GET: updateRelativeBeatyandHealthies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var updateRelativeBeatyandHealthy = await _context.updateRelativeBeatyandHealthy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (updateRelativeBeatyandHealthy == null)
            {
                return NotFound();
            }

            return View(updateRelativeBeatyandHealthy);
        }

        // GET: updateRelativeBeatyandHealthies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: updateRelativeBeatyandHealthies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageOrvideo,subject,Address,BeatyId")] updateRelativeBeatyandHealthy updateRelativeBeatyandHealthy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(updateRelativeBeatyandHealthy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(updateRelativeBeatyandHealthy);
        }

        // GET: updateRelativeBeatyandHealthies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var updateRelativeBeatyandHealthy = await _context.updateRelativeBeatyandHealthy.FindAsync(id);
            if (updateRelativeBeatyandHealthy == null)
            {
                return NotFound();
            }
            return View(updateRelativeBeatyandHealthy);
        }

        // POST: updateRelativeBeatyandHealthies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ImageOrvideo,subject,Address,BeatyId")] updateRelativeBeatyandHealthy updateRelativeBeatyandHealthy)
        {
            if (id != updateRelativeBeatyandHealthy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updateRelativeBeatyandHealthy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!updateRelativeBeatyandHealthyExists(updateRelativeBeatyandHealthy.Id))
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
            return View(updateRelativeBeatyandHealthy);
        }

        // GET: updateRelativeBeatyandHealthies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var updateRelativeBeatyandHealthy = await _context.updateRelativeBeatyandHealthy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (updateRelativeBeatyandHealthy == null)
            {
                return NotFound();
            }

            return View(updateRelativeBeatyandHealthy);
        }

        // POST: updateRelativeBeatyandHealthies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var updateRelativeBeatyandHealthy = await _context.updateRelativeBeatyandHealthy.FindAsync(id);
            _context.updateRelativeBeatyandHealthy.Remove(updateRelativeBeatyandHealthy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool updateRelativeBeatyandHealthyExists(string id)
        {
            return _context.updateRelativeBeatyandHealthy.Any(e => e.Id == id);
        }
    }
}
