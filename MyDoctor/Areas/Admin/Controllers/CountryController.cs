using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;

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
            var countries = await _countryRepository.GetAll().Include(c=>c.Cities).ToListAsync();
            return View(countries);
        }
        public async Task<IActionResult> CreateEdit(Country country) {
            if (ModelState.IsValid)
            {
                if (country.Id > 0)
                {
                    await _countryRepository.UpdateAsync(country);
                    AddMessage("Country Update Success", true);
                }
                else
                {
                    await _countryRepository.InsertAsync(country);
                    AddMessage("Country Create Success", true);
                }
            }
            else {
                AddError(ModelState);
            }
            
               
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _countryRepository.DeleteAsync(id);
                AddMessage("Country Delete Success", true);


            }
            catch (Exception e)
            {
                AddMessage("Country Delete Failed");

            }
            return RedirectToAction(nameof(Index));
        }
    }
}