using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class BeatyandHealthiesController : Controller
    {
        private readonly ICategoryRepository _beatyandHealthRepository;

        public BeatyandHealthiesController(ICategoryRepository beatyandHealthRepository)=>
            _beatyandHealthRepository = beatyandHealthRepository;
        

        public IActionResult HeartBlus()=>View();
        public IActionResult Bmi()=> View();
       

        // GET: BeatyandHealthies
        public async Task<IActionResult> Index()
        {
            var beatyHealth = await _beatyandHealthRepository.GetAllAsync();
            return View(beatyHealth);
        }

        [HttpGet]
        public async Task<IActionResult> Indexincreatepost()
        {
            var beatyHealth = await _beatyandHealthRepository.GetAllAsync();
            return new JsonResult(beatyHealth);
        }

        // GET: BeatyandHealthies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var beatyandHealthy = await _beatyandHealthRepository.GetByIdAsync(id);
            if (beatyandHealthy == null) return NotFound();
            return View(beatyandHealthy);
        }

        // GET: BeatyandHealthies/Create
        public IActionResult Create()=>View();
        

        // POST: BeatyandHealthies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Image")] BeatyandHealthy beatyandHealthy)
        {
            if (ModelState.IsValid)
            {
                await _beatyandHealthRepository.InsertAsync(beatyandHealthy);
                return RedirectToAction(nameof(Index));
            }

            return View(beatyandHealthy);
        }

        // GET: BeatyandHealthies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var beatyandHealthy = await _beatyandHealthRepository.GetByIdAsync(id);
            if (beatyandHealthy == null) return NotFound();
            return View(beatyandHealthy);
        }

        // POST: BeatyandHealthies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Image")] BeatyandHealthy beatyandHealthy)
        {
            if (id != beatyandHealthy.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _beatyandHealthRepository.Update(beatyandHealthy);
                return RedirectToAction(nameof(Index));
            }

            return View(beatyandHealthy);
        }

        // GET: BeatyandHealthies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var beatyandHealthy = await _beatyandHealthRepository.GetByIdAsync(id);
            if (beatyandHealthy == null) return NotFound();
            return View(beatyandHealthy);
        }

        // POST: BeatyandHealthies/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _beatyandHealthRepository.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}