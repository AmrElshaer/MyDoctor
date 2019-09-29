using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    [Authorize]
    public class DiseasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiseasesController(ApplicationDbContext context)
        {
            _context = context;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Disease.ToListAsync());
        }
        [HttpGet]
        public IActionResult actionResulttogetalldisease()
        {
            return new JsonResult(_context.Disease.ToList());
        }
        // GET: Diseases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = await _context.Disease
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        // GET: Diseases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiseaseName,Subject,Image,Protection,Medicin,Reasons")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(disease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disease);
        }

        // GET: Diseases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = await _context.Disease.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiseaseName,Subject,Image,Protection,Medicin,Reasons")] Disease disease)
        {
            if (id != disease.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseExists(disease.Id))
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
            return View(disease);
        }

        // GET: Diseases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = await _context.Disease
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disease = await _context.Disease.FindAsync(id);
            _context.Disease.Remove(disease);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseExists(int id)
        {
            return _context.Disease.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task AppComment(string Commment, string DiseaseName)
        {
            Comments obj = new Comments();
            obj.Commment = Commment;
            CutomPropertiy cutom = this._context.Users.FirstOrDefault(a => a.UserName == User.Identity.Name);
            if(cutom.ImagePath == null)
            {

                cutom.ImagePath = "Defulat.jpg";
            }
            obj.ImagePath = cutom.ImagePath;
            obj.UserName = cutom.UserName;
            obj.DiseaseName = DiseaseName;

            await this._context.Comments.AddAsync(obj);
            this._context.SaveChanges();

        }
        //DiseaseComments
        [HttpGet]
        public JsonResult AllComment(string DiseaseName)
        {
            List<Comments> allcomments = this._context.Comments.Where(a => a.DiseaseName == DiseaseName).ToList();
            return Json(allcomments);
        }
        //Like and Dislike
        public JsonResult LikeAndDislike(string DiseaseName)
        {
            LikeandDislikeclass likeAndDislike = new LikeandDislikeclass ();
            return Json(likeAndDislike);
        }
        //details for relativeDiseases
        public async Task<IActionResult> RelativeDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relativeDisease = await _context.RelativeDisease
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relativeDisease == null)
            {
                return NotFound();
            }

            return View(relativeDisease);
        }

    }
}
