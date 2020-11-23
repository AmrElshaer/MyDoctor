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
                );
              var subset = searchHits.Skip((searchParamter.Page.Value - 1) * searchParamter.PageSize).Take(searchParamter.PageSize);
              searchParamter.TotalCount=searchHits.Count();
            var searchResult = new SearchResult<Medicin>()
            {
                ItemsList =subset.Select(m => new Medicin()
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

                }),
                SearchParamter=searchParamter
            };
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
                Doctors = (medicin.BeatyandHealthy.Doctors.Any(), medicin.BeatyandHealthy.Doctors.Count() >= numberRelated) == (true, true) ?
                           medicin.BeatyandHealthy.Doctors :
                           medicin.BeatyandHealthy.Doctors.AppendData(await _context.Doctor.Include( d => d.Category).Where(d => d.Category.Id != medicin.BeatyandHealthyId).Take(numberRelated - medicin.BeatyandHealthy.Doctors.Count()).ToListAsync()),
                Medicins = await _context.Medicin.Where(m=>m.Id!=id).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Diseases = (medicin.BeatyandHealthy.Diseases.Any(), medicin.BeatyandHealthy.Diseases.Count() >= numberRelated) == (true, true) ?
                           medicin.BeatyandHealthy.Diseases :
                           medicin.BeatyandHealthy.Diseases.AppendData(await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != medicin.BeatyandHealthyId).Take(numberRelated - medicin.BeatyandHealthy.Diseases.Count()).ToListAsync()),
                RelativeCategories = (medicin.BeatyandHealthy.RelativeofBeatyandhealthies.Any(), medicin.BeatyandHealthy.RelativeofBeatyandhealthies.Count() >= numberRelated) == (true, true) ?
                                     medicin.BeatyandHealthy.RelativeofBeatyandhealthies :
                                      medicin.BeatyandHealthy.RelativeofBeatyandhealthies.AppendData(await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != medicin.BeatyandHealthyId).Take(numberRelated - medicin.BeatyandHealthy.RelativeofBeatyandhealthies.Count()).ToListAsync())
            };
            return result;
        }
    }
}
