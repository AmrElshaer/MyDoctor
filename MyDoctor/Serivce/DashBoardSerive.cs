using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.IRepository;
using MyDoctor.ISerivce;
using MyDoctor.Models;
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
                Categories = await _categoryRepository.GetAll().Take(pageSize).ToListAsync(),
                Doctors = await _doctorRepository.GetAll(includes: new List<Expression<Func<Doctor, object>>> { rc => rc.Category }).Take(pageSize).ToListAsync(),
                Medicins = await _medicinRepository.GetAll(includes: new List<Expression<Func<Medicin, object>>> { rc => rc.BeatyandHealthy }).Take(pageSize).ToListAsync(),
                Diseases = await _diseasesRepository.GetAll(includes: new List<Expression<Func<Disease, object>>> { rc => rc.BeatyandHealthy }).Take(pageSize).ToListAsync(),
                RelativeCategories = await _categoryRelativiesRepository.GetAll(includes:new List<Expression<Func<RelativeofBeatyandhealthy, object>>> { rc=>rc.BeatyandHealthy}).Take(pageSize).ToListAsync()
            };
            return result;
        }
    }
}
