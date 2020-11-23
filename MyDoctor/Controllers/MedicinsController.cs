using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;

namespace MyDoctor.Controllers
{
    public class MedicinsController : Controller
    {
        private readonly IMedicinRepository _medicinRepository;
        public MedicinsController(IMedicinRepository medicinRepository)
        {
            _medicinRepository = medicinRepository;
        }
        public async Task<IActionResult> Index(MedicinSearch medicinSearch) {
            var medicins = await _medicinRepository.GEtMedicinsAsync(medicinSearch);
            return View(new MedicinViewModel(medicins, medicinSearch));

        }
        public async Task<IActionResult> Details(int id) {
            var medicin = await _medicinRepository.GetMedicinAsync(id,4);
            return View(medicin);
        
        }
    }
}
