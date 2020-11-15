using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.IRepository;
using MyDoctor.ISerivce;
using MyDoctor.ViewModels;

namespace MyDoctor.Serivce
{
    public class CategorySerivce:ICategorySerivce
    {
        private readonly ICategoryRepository _categoryRepository;
   
        public CategorySerivce(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
           
        }
        public async Task<IBaseViewModel> GetCategory(int categoryId)
        {
          
            var cateogry = await _categoryRepository.GetByIdAsync(categoryId,c => c.Diseases, c => c.Medicins, c => c.Doctors, c => c.RelativeofBeatyandhealthies);
            var result = new BeatyandHealthViewModel()
            {
                BeatyandHealthy =cateogry,
                Categories = await  _categoryRepository.GetAll(a=>a.Id!=categoryId).ToListAsync(),
                Doctors = cateogry.Doctors,
                Medicins = cateogry.Medicins,
                Diseases = cateogry.Diseases,
                RelativeCategories = cateogry.RelativeofBeatyandhealthies
            };
            return result;
        }
    }
}
