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
    public class DashBoardSerivce:IDashBoardSerivce
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMedicinRepository _medicinRepository;
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly ICategoryRelativiesRepository _categoryRelativiesRepository;
        public DashBoardSerivce(ICategoryRepository categoryRepository, IDoctorRepository doctorRepository, IMedicinRepository medicinRepository, IDiseasesRepository diseasesRepository, ICategoryRelativiesRepository categoryRelativiesRepository)
        {
            _categoryRepository = categoryRepository;
            _doctorRepository = doctorRepository;
            _medicinRepository = medicinRepository;
            _diseasesRepository = diseasesRepository;
            _categoryRelativiesRepository = categoryRelativiesRepository;
        }
        public async  Task<IBaseViewModel> GetBoardViewModel(int pageSize)
        {
            var result = new DashBoardViewModel()
            {
                Categories = await _categoryRepository.Search().Take(pageSize).ToListAsync(),
                Doctors = await _doctorRepository.Search().Take(pageSize).ToListAsync(),
                Medicins = await _medicinRepository.Search().Take(pageSize).ToListAsync(),
                Diseases = await _diseasesRepository.Search().Take(pageSize).ToListAsync(),
                RelativeCategories = await _categoryRelativiesRepository.Search().Take(pageSize).ToListAsync()
            };
            return result;
        }
    }
}
