using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.IRepository;
using MyDoctor.Models;
using MyDoctor.ViewModels;

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
            var medicins = await _medicinRepository.GetAll(
               d => (!medicinSearch.Categories.Any() || medicinSearch.Categories.Contains(d.BeatyandHealthyId))
               && (string.IsNullOrEmpty(medicinSearch.Name) || d.Name.ToLower().Contains(medicinSearch.Name.ToLower()))
               &&(!medicinSearch.Price.HasValue||d.Price<=medicinSearch.Price.Value)
                , d => d.OrderByDescending(o => o.Id), d => d.BeatyandHealthy).ToListAsync();
            var pricerange = _medicinRepository.PriceRange();
            medicinSearch.MinPrice = pricerange.minprice;
            medicinSearch.MaxPrice = pricerange.maxprice;
            return View(new MedicinViewModel(medicins, medicinSearch));

        }
        public async Task<IActionResult> Details(int id) {
            var medicin = await _medicinRepository.GetMedicinAsync(id,4);
            return View(medicin);
        
        }
    }
}
