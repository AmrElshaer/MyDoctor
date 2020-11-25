using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;

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
            if (ModelState.IsValid)
            {
                if (city.Id > 0)
                {
                    await _cityRepository.UpdateAsync(city);
                    AddMessage("City Update Success", true);
                }
                else
                {
                    await _cityRepository.InsertAsync(city);
                    AddMessage("City Create Success", true);
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
                await _cityRepository.DeleteAsync(id);
                AddMessage("City Delete Success", true);


            }
            catch (Exception e)
            {
                AddMessage("City Delete Failed");

            }
            return RedirectToAction(nameof(Index));
        }
    }
}