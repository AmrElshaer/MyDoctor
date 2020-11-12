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
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMedicinRepository _medicinRepository;
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly ICategoryRelativiesRepository _categoryRelativiesRepository;
        public CategorySerivce(ICategoryRepository categoryRepository, IDoctorRepository doctorRepository, IMedicinRepository medicinRepository, IDiseasesRepository diseasesRepository, ICategoryRelativiesRepository categoryRelativiesRepository)
        {
            _categoryRepository = categoryRepository;
            _doctorRepository = doctorRepository;
            _medicinRepository = medicinRepository;
            _diseasesRepository = diseasesRepository;
            _categoryRelativiesRepository = categoryRelativiesRepository;
        }
        public async Task<IBaseViewModel> GetCategory(int categoryId)
        {
            var cateogry = await _categoryRepository.GetCategoryWithRelated(categoryId);
            var result = new BeatyandHealthViewModel()
            {
                BeatyandHealthy =cateogry,
                Categories = await  _categoryRepository.GetAllAsync(a=>a.Id!=categoryId),
                Doctors = cateogry.Doctors,
                Medicins = cateogry.Medicins,
                Diseases = cateogry.Diseases,
                RelativeCategories = cateogry.RelativeofBeatyandhealthies
            };
            return result;
        }
    }
}
