using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class CountryController : BaseController
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var countries = await _countryRepository.GetAll(null,null,c=>c.Cities).ToListAsync();
            return View(countries);
        }
        public async Task<IActionResult> CreateEdit(Country country) {
            if (country.Id > 0)
            {
                await _countryRepository.UpdateAsync(country);
                AddMessage("Country Update Success", "Message", true);
            }
            else { 
                  await  _countryRepository.InsertAsync(country);
                 AddMessage("Country Create Success", "Message",true);
            }
               
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _countryRepository.DeleteAsync(id);
                AddMessage("Country Delete Success", "Message", true);


            }
            catch (Exception e)
            {
                AddMessage("Country Delete Failed", "Message");

            }
            return RedirectToAction(nameof(Index));
        }
    }
}