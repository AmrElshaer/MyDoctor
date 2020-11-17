using System;
using MyDoctor.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MyDoctor.Areas.Admin.Controllers
{
    public class CityController : BaseController
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public IActionResult Index()=>RedirectToAction(actionName:"Index",controllerName:"Country");
        
        public async Task<IActionResult> CreateEdit(City city)
        {
            if (city.Id > 0)
            {
                await _cityRepository.UpdateAsync(city);
                AddMessage("City Update Success", "Message", true);
            }
            else
            {
                await _cityRepository.InsertAsync(city);
                AddMessage("City Create Success", "Message", true);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _cityRepository.DeleteAsync(id);
                AddMessage("City Delete Success", "Message", true);


            }
            catch (Exception e)
            {
                AddMessage("City Delete Failed", "Message");

            }
            return RedirectToAction(nameof(Index));
        }
    }
}