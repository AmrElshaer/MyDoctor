﻿using System.Threading.Tasks;
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
            return View( await _medicinRepository.GEtMedicinsAsync(medicinSearch));
        }
        public async Task<IActionResult> Details(int? id) {
            if (id == null)
            {
                return NotFound();
            }
            var medicin = await _medicinRepository.GetMedicinAsync(id.Value,4);
            return View(medicin);
        
        }
    }
}
