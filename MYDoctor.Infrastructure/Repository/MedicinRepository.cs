using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Core.Application.Common;
using MYDoctor.Infrastructure.Identity;
using System.Collections;
using System.Collections.Generic;
using MYDoctor.Infrastructure.Helper;

namespace MYDoctor.Infrastructure.Repository
{
    public class MedicinRepository:BaseRepository<Medicin>,IMedicinRepository
    {
      
        public MedicinRepository(ApplicationDbContext context) : base(context)
        {
           
        }

        public SearchResult<Medicin> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(x =>
               (string.IsNullOrEmpty(searchParamter.SearchQuery)|| x.Name.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
               && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
               && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo)
                , m => m.OrderByDescending(a => a.Id),
                m=>m.BeatyandHealthy,
                m=>m.DiseaseMedicins
                ).Select(m => new Medicin()
                {
                    Name = m.Name,
                    Affects = m.Affects,
                    Indications = m.Indications,
                    Id = m.Id,
                    Image = m.Image,
                    BeatyandHealthy = m.BeatyandHealthy,
                    Price = m.Price,
                    DiseaseMedicins = m.DiseaseMedicins,
                    CreateDate = m.CreateDate

                });
             
            var searchResult = PagingHelper.PagingModel(searchHits,searchParamter);
            return searchResult;
        }

        public async Task CreateEdit(Medicin medicin)
        {
            if (medicin.Id >0)
            {
               
                medicin.ModifiedDate = DateTime.Now.Date;
                var medicine =await _table.Include(m=>m.DiseaseMedicins).FirstOrDefaultAsync(m=>m.Id==medicin.Id);
                //Delete If NOt Longer Exist
                medicine.DiseaseMedicins.ToList().ForEach(dm=>{
                    if (medicin.DiseaseMedicins.All(a => a.DiseaseId != dm.DiseaseId))
                             medicine.DiseaseMedicins.Remove(dm);
                });
                //Add If Not Exist
                var medicineComparer=new MedicineComparer();
                  medicin.DiseaseMedicins.ToList().ForEach(dm=>
                  {
                      if (!medicine.DiseaseMedicins.Contains(dm, medicineComparer))
                         medicine.DiseaseMedicins.Add(dm);
                  });
                //Update Only Simple Properties
                _context.Entry(medicine).CurrentValues.SetValues(medicin);
                await _context.SaveChangesAsync();
               

            }
            else
            {
                medicin.CreateDate = DateTime.Now.Date;
                await InsertAsync(medicin);


            }
           
        }
        public async Task<IEnumerable<Medicin>> GEtMedicinsAsync(MedicinSearch medicinSearch) {
            var medicins = await GetAll(
              d => (!medicinSearch.Categories.Any() || medicinSearch.Categories.Contains(d.BeatyandHealthyId))
              && (string.IsNullOrEmpty(medicinSearch.Name) || d.Name.ToLower().Contains(medicinSearch.Name.ToLower()))
              && (!medicinSearch.Price.HasValue || d.Price <= medicinSearch.Price.Value)
               , d => d.OrderByDescending(o => o.Id), d => d.BeatyandHealthy).ToListAsync();
            var pricerange = PriceRange();
            medicinSearch.MinPrice = pricerange.minprice;
            medicinSearch.MaxPrice = pricerange.maxprice;
            return medicins;

        }
        public (decimal minprice, decimal maxprice) PriceRange()
        {
            var maxprice= _context.Medicin.Max(m=>m.Price);
            var minprice= _context.Medicin.Min(m=>m.Price);
            return (minprice, maxprice);
        }

        public async Task<MedicinViewModel> GetMedicinAsync(int id,int numberRelated)
        {
            var medicin =await  _context.Medicin.Include(m=>m.BeatyandHealthy).Include(m=>m.DiseaseMedicins).ThenInclude(dm=>dm.Disease).FirstOrDefaultAsync(m=>m.Id==id);

            var result = new MedicinViewModel()
            {
                Medicin = medicin,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != medicin.BeatyandHealthyId).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Doctors = await GetRelativesDoctors(medicin,numberRelated),
                Medicins = await GetRelativesMedicins(medicin,numberRelated),
                Diseases = await GetRelativesDiseases(medicin,numberRelated),
                RelativeCategories = await GetRelativesCategories(medicin,numberRelated)
            };
            return result;
        }
        private async Task<IEnumerable<Doctor>> GetRelativesDoctors(Medicin medicin, int numberRelated)
        {
            var relativeDoctors = medicin.BeatyandHealthy.Doctors;
            if ((relativeDoctors.Any(), relativeDoctors.Count() >= numberRelated) == (true, true))
                return relativeDoctors;
            var doctors = await _context.Doctor.Include(d => d.Category).Where(d => d.CategoryId != medicin.BeatyandHealthyId).Take(numberRelated - relativeDoctors.Count()).ToListAsync();
            return relativeDoctors.AppendData(doctors);
        }
        private async Task<IEnumerable<Medicin>> GetRelativesMedicins(Medicin medicin, int numberRelated)
        {
            var relativeMedicin = medicin.BeatyandHealthy.Medicins;
            if ((relativeMedicin.Any(), relativeMedicin.Count() >= numberRelated) == (true, true))
                return relativeMedicin;
            var medicins = await _context.Medicin.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != medicin.BeatyandHealthyId).Take(numberRelated - relativeMedicin.Count()).ToListAsync();
            return relativeMedicin.AppendData(medicins);
        }
        private async Task<IEnumerable<Disease>> GetRelativesDiseases(Medicin medicin, int numberRelated)
        {
            var relativeDisease = medicin.BeatyandHealthy.Diseases;
            if ((relativeDisease.Any(), relativeDisease.Count() >= numberRelated) == (true, true))
                return relativeDisease;
            var medicins = await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != medicin.BeatyandHealthyId).Take(numberRelated - relativeDisease.Count()).ToListAsync();
            return relativeDisease.AppendData(medicins);
        }
        private async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategories(Medicin medicin, int numberRelated)
        {
            var relativeCategories = medicin.BeatyandHealthy.RelativeofBeatyandhealthies;
            if ((relativeCategories.Any(), relativeCategories.Count() >= numberRelated) == (true, true))
                return relativeCategories;
            var medicins = await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthId != medicin.BeatyandHealthyId).Take(numberRelated - relativeCategories.Count()).ToListAsync();
            return relativeCategories.AppendData(medicins);
        }

    }
}
