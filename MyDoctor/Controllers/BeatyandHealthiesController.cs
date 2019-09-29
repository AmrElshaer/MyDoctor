using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Models;
using MyDoctor.ViewModels;

namespace MyDoctor.Controllers
{
    public class BeatyandHealthiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeatyandHealthiesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult HeartBlus()
        {
            return View();
        }
        public IActionResult Bmi()
        {
            return View();
        }
        // GET: BeatyandHealthies
        public async Task<IActionResult> Index()
        {
            return View(await _context.BeatyandHealthy.ToListAsync());
        }
        [HttpGet]
        public IActionResult Indexincreatepost()
        {
            return new JsonResult(_context.BeatyandHealthy.ToList());
        }
        // GET: BeatyandHealthies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var beatyandHealthy =await _context.BeatyandHealthy
                .FirstOrDefaultAsync(m => m.Id == id);
           
            if (beatyandHealthy == null)
            {
                return NotFound();
            }
            
             
            
            return View(beatyandHealthy);
        }

        // GET: BeatyandHealthies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BeatyandHealthies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Catagory,Image")] BeatyandHealthy beatyandHealthy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beatyandHealthy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beatyandHealthy);
        }

        // GET: BeatyandHealthies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beatyandHealthy = await _context.BeatyandHealthy.FindAsync(id);
            if (beatyandHealthy == null)
            {
                return NotFound();
            }
            return View(beatyandHealthy);
        }

        // POST: BeatyandHealthies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Catagory,Image")] BeatyandHealthy beatyandHealthy)
        {
            if (id != beatyandHealthy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beatyandHealthy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeatyandHealthyExists(beatyandHealthy.Id))
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
            return View(beatyandHealthy);
        }

        // GET: BeatyandHealthies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beatyandHealthy = await _context.BeatyandHealthy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beatyandHealthy == null)
            {
                return NotFound();
            }

            return View(beatyandHealthy);
        }

        // POST: BeatyandHealthies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beatyandHealthy = await _context.BeatyandHealthy.FindAsync(id);
            _context.BeatyandHealthy.Remove(beatyandHealthy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeatyandHealthyExists(int id)
        {
            return _context.BeatyandHealthy.Any(e => e.Id == id);
        }
    }
}
