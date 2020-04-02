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
    public class CommentfordoctorpostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentfordoctorpostsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Commentfordoctorposts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commentfordoctorpost.ToListAsync());
        }

        // GET: Commentfordoctorposts/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentfordoctorpost =  _context.Commentfordoctorpost.Where(m => m.postid == id.ToString());
            if (commentfordoctorpost == null)
            {
                return NotFound();
            }

            return new JsonResult(commentfordoctorpost);
        }

        // GET: Commentfordoctorposts/Create
        public IActionResult Create(string doctorid,string Id)
        {
            ViewData["doctorid"] = doctorid;
            ViewData["id"] = Id;
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,containt,userid,postid")] Commentfordoctorpost commentfordoctorpost)
        {
            if (ModelState.IsValid)
            {
                commentfordoctorpost.id = 0;
                _context.Add(commentfordoctorpost);
                await _context.SaveChangesAsync();
                return Redirect("/Posts/Details?id=" + commentfordoctorpost.postid);
            }
            return Redirect("/Posts/Details?id="+commentfordoctorpost.postid);
        }

        // GET: Commentfordoctorposts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentfordoctorpost = await _context.Commentfordoctorpost.FindAsync(id);
            if (commentfordoctorpost == null)
            {
                return NotFound();
            }
            return View(commentfordoctorpost);
        }

        // POST: Commentfordoctorposts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,containt,userid,postid")] Commentfordoctorpost commentfordoctorpost)
        {
            if (id != commentfordoctorpost.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentfordoctorpost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentfordoctorpostExists(commentfordoctorpost.id))
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
            return View(commentfordoctorpost);
        }

        // GET: Commentfordoctorposts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentfordoctorpost = await _context.Commentfordoctorpost
                .FirstOrDefaultAsync(m => m.id == id);
            if (commentfordoctorpost == null)
            {
                return NotFound();
            }

            return View(commentfordoctorpost);
        }

        // POST: Commentfordoctorposts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentfordoctorpost = await _context.Commentfordoctorpost.FindAsync(id);
            _context.Commentfordoctorpost.Remove(commentfordoctorpost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentfordoctorpostExists(int id)
        {
            return _context.Commentfordoctorpost.Any(e => e.id == id);
        }
    }
}
