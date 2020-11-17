using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Helper;
using MyDoctor.IRepository;
using MyDoctor.ISerivce;
using MyDoctor.Models;
using MyDoctor.ViewModels;

namespace MyDoctor.Serivce
{
    public class CategorySerivce:ICategorySerivce
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly IMedicinRepository _medicinRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ICategoryRelativiesRepository _categoryRelativiesRepository;


        public CategorySerivce(ICategoryRepository categoryRepository,IDiseasesRepository diseasesRepository,IMedicinRepository medicinRepository,IDoctorRepository doctorRepository,ICategoryRelativiesRepository categoryRelativiesRepository)
        {
            _categoryRepository = categoryRepository;
            _diseasesRepository = diseasesRepository;
            _medicinRepository = medicinRepository;
            _doctorRepository = doctorRepository;
            _categoryRelativiesRepository = categoryRelativiesRepository;
        }
        public async Task<IBaseViewModel> GetCategory(int categoryId,int numberRelated)
        {
          
            var cateogry = await _categoryRepository.GetByIdAsync(categoryId,c => c.Diseases, c => c.Medicins, c => c.Doctors, c => c.RelativeofBeatyandhealthies);
         
            var result = new BeatyandHealthViewModel()
            {
                BeatyandHealthy =cateogry,
                Categories = await  _categoryRepository.GetAll(a=>a.Id!=categoryId).ToListAsync(),
                Doctors = (cateogry.Doctors.Any(),cateogry.Doctors.Count()>=numberRelated) ==(true,true)? 
                           cateogry.Doctors:
                           cateogry.Doctors.AppendData(await _doctorRepository.GetAll(expression:d=>d.Category.Id!=categoryId,includes:d=>d.Category).Take(numberRelated- cateogry.Doctors.Count()).ToListAsync()),
                Medicins = (cateogry.Medicins.Any(),cateogry.Medicins.Count()>= numberRelated)==(true,true) ? 
                           cateogry.Medicins :
                           cateogry.Medicins.AppendData(await _medicinRepository.GetAll(expression: d => d.BeatyandHealthy.Id != categoryId, includes:  d => d.BeatyandHealthy).Take(numberRelated- cateogry.Medicins.Count()).ToListAsync()),
                Diseases = (cateogry.Diseases.Any(),cateogry.Diseases.Count()>= numberRelated)==(true,true) ? 
                           cateogry.Diseases :
                           cateogry.Diseases.AppendData( await _diseasesRepository.GetAll(expression: d => d.BeatyandHealthy.Id != categoryId, includes:  d => d.BeatyandHealthy ).Take(numberRelated-cateogry.Diseases.Count()).ToListAsync()),
                RelativeCategories = (cateogry.RelativeofBeatyandhealthies.Any(),cateogry.RelativeofBeatyandhealthies.Count()>= numberRelated)==(true,true) ? 
                                     cateogry.RelativeofBeatyandhealthies : 
                                      cateogry.RelativeofBeatyandhealthies.AppendData(await _categoryRelativiesRepository.GetAll(expression: d => d.BeatyandHealthy.Id != categoryId, includes: d => d.BeatyandHealthy).Take(numberRelated-cateogry.RelativeofBeatyandhealthies.Count()).ToListAsync())
            };
            return result;
        }
    }
}
