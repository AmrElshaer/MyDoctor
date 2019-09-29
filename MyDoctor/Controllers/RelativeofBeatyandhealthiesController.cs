using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyDoctor.Data;
using MyDoctor.Models;
using MyDoctor.ViewModels;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MyDoctor.Controllers
{
    public class RelativeofBeatyandhealthiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelativeofBeatyandhealthiesController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        // GET: RelativeofBeatyandhealthies
        public async Task<IActionResult> Index(int id)
        {
           var data= _context.RelativeofBeatyandhealthy.Where(a=>a.BeatyId==id);
            return Json(data);
        }
        
         
    // GET: RelativeofBeatyandhealthies/Details/5
    public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
             var relativeofBeatyandhealthy = await _context.RelativeofBeatyandhealthy
                .FirstOrDefaultAsync(m => m.Id == id);

            if (relativeofBeatyandhealthy == null)
            {
                return NotFound();
            }

            return View(relativeofBeatyandhealthy);
        }

        // GET: RelativeofBeatyandhealthies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelativeofBeatyandhealthies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageOrvideo,subject,Address,BeatyId")] RelativeofBeatyandhealthy relativeofBeatyandhealthy)
        {
            if (ModelState.IsValid)
            {
               


                _context.Add(relativeofBeatyandhealthy);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(relativeofBeatyandhealthy);
        }

        // GET: RelativeofBeatyandhealthies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relativeofBeatyandhealthy = await _context.RelativeofBeatyandhealthy.FindAsync(id);
            if (relativeofBeatyandhealthy == null)
            {
                return NotFound();
            }
            return View(relativeofBeatyandhealthy);
        }

        // POST: RelativeofBeatyandhealthies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ImageOrvideo,subject,Address")] RelativeofBeatyandhealthy relativeofBeatyandhealthy)
        {
            if (id != relativeofBeatyandhealthy.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relativeofBeatyandhealthy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelativeofBeatyandhealthyExists(relativeofBeatyandhealthy.Id))
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
            return View(relativeofBeatyandhealthy);
        }

        // GET: RelativeofBeatyandhealthies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relativeofBeatyandhealthy = await _context.RelativeofBeatyandhealthy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relativeofBeatyandhealthy == null)
            {
                return NotFound();
            }

            return View(relativeofBeatyandhealthy);
        }

        // POST: RelativeofBeatyandhealthies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var relativeofBeatyandhealthy = await _context.RelativeofBeatyandhealthy.FindAsync(id);
            _context.RelativeofBeatyandhealthy.Remove(relativeofBeatyandhealthy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelativeofBeatyandhealthyExists(string id)
        {
            return _context.RelativeofBeatyandhealthy.Any(e => e.Id == id);
        }
    }
}
