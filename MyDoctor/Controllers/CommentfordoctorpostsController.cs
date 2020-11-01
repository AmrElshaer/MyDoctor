
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class CommentfordoctorpostsController : Controller
    {
        private readonly ICommentForDoctorPostRepository _commentPostRepository;

        public CommentfordoctorpostsController( ICommentForDoctorPostRepository commentPostRepository)
        {
            _commentPostRepository = commentPostRepository;
            
        }

        // GET: Commentfordoctorposts
        public async Task<IActionResult> Index()
        {
            var comments = await _commentPostRepository.GetAllAsync();
            return View(comments);
        }

        // GET: Commentfordoctorposts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var commentfordoctorpost =await _commentPostRepository.GetByIdAsync(id);
            if (commentfordoctorpost == null) return NotFound();

            return Json(commentfordoctorpost);
        }

        // GET: Commentfordoctorposts/Create
        public IActionResult Create(string doctorid, string id)
        {
            ViewData["doctorid"] = doctorid;
            ViewData["id"] =id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("id,containt,userid,postid")] Commentfordoctorpost commentfordoctorpost)
        {
            if (ModelState.IsValid)
            {
                commentfordoctorpost.id = 0;
                await _commentPostRepository.InsertAsync(commentfordoctorpost);
                
                return Redirect("/Posts/Details?id=" + commentfordoctorpost.postid);
            }

            return Redirect($"/Posts/Details?id={commentfordoctorpost.postid}");
        }

        // GET: Commentfordoctorposts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var commentfordoctorpost =  await _commentPostRepository.GetByIdAsync(id);
            if (commentfordoctorpost == null) return NotFound();
            return View(commentfordoctorpost);
        }

        // POST: Commentfordoctorposts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("id,containt,userid,postid")] Commentfordoctorpost commentfordoctorpost)
        {
            

            if (ModelState.IsValid)
            {
               
                    await _commentPostRepository.Update(commentfordoctorpost);
                    
                    return RedirectToAction(nameof(Index));
            }

            return View(commentfordoctorpost);
        }

        // GET: Commentfordoctorposts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var commentfordoctorpost = await _commentPostRepository.GetByIdAsync(id);
            if (commentfordoctorpost == null) return NotFound();

            return View(commentfordoctorpost);
        }

        // POST: Commentfordoctorposts/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _commentPostRepository.DeleteAsync(id);
            
             return RedirectToAction(nameof(Index));
        }

       
    }
}