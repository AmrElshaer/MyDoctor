
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Helper;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    [Authorize]
    public class DiseasesController : Controller
    {
       
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IDiseaseRelativeRepository _diseaseRelativeRepository;
        public DiseasesController(IDiseasesRepository diseasesRepository, ICommentRepository commentRepository, IDiseaseRelativeRepository diseaseRelativeRepository)
        {
            _diseasesRepository = diseasesRepository;
            _commentRepository = commentRepository;
            _diseaseRelativeRepository = diseaseRelativeRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var disease = await _diseasesRepository.GetAllAsync();
            return View(disease);
        }
        [HttpGet]
        public async Task<IActionResult>  actionResulttogetalldisease()
        {
            var disease = await _diseasesRepository.GetAllAsync();
            return new JsonResult(disease);
        }
        // GET: Diseases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var disease = await _diseasesRepository.GetByIdAsync(id);
            if (disease == null) return NotFound();

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

                 await _diseasesRepository.InsertAsync(disease);
                 await _diseasesRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disease);
        }

        // GET: Diseases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var disease = await _diseasesRepository.GetByIdAsync(id);
            if (disease == null) return NotFound();
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiseaseName,Subject,Image,Protection,Medicin,Reasons")] Disease disease)
        {
            if (id != disease.Id) return NotFound();

            if (ModelState.IsValid)
            {
               
                   _diseasesRepository.Update(disease);
                   await _diseasesRepository.SaveAsync();
               

                  return RedirectToAction(nameof(Index));
            }

            return View(disease);
        }

        // GET: Diseases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var disease = await _diseasesRepository.GetByIdAsync(id);
            if (disease == null) return NotFound();

            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _diseasesRepository.DeleteAsync(id);
            await _diseasesRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

      
        [HttpGet]
        public async Task AppComment(DiseaseHelper diseaseHelper)
        {

            await _commentRepository.InsertCommentAsync(diseaseHelper, User.Identity.Name);

        }
        //DiseaseComments
        [HttpGet]
        public async Task<JsonResult> AllComment(string diseaseName)
        {

            var  commencements =await _commentRepository.GetAllAsync(
                comment=>comment.DiseaseName.ToLower().Contains(diseaseName.ToLower())
                );
            return Json(commencements);
        }
        //Like and Dislike
        public JsonResult LikeAndDislike(string diseaseName)
        {
            LikeandDislikeclass likeAndDislike = new LikeandDislikeclass ();
            return Json(likeAndDislike);
        }
        //details for relativeDiseases
        public async Task<IActionResult> RelativeDetails(string id)
        {
            if (id == null) return NotFound();
            var relativeDisease = await _diseaseRelativeRepository.GetByIdAsync(id);
            if (relativeDisease == null) return NotFound();
            return View(relativeDisease);
        }

    }
}
