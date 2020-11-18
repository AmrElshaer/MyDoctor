using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Data;
using MyDoctor.Helper;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
using MyDoctor.ViewModels;
using PagedList.Core;

namespace MyDoctor.Repository
{
    public class MedicinRepository:BaseRepository<Medicin>,IMedicinRepository
    {
      
        public MedicinRepository(ApplicationDbContext context) : base(context)
        {
           
        }

        public SearchResult<Medicin> GetSearchResult(string query, int page, int pageSize, DateTime? createFrom, DateTime? createTo)
        {
            var searchHits = GetAll(x =>
               (query == null || x.Name.ToLower().Contains(query.ToLower()))
               && (createFrom == null || x.CreateDate >= createFrom)
               && (createTo == null || x.CreateDate <= createTo)
                , m => m.OrderByDescending(a => a.Id),
                m=>m.BeatyandHealthy,
                m=>m.DiseaseMedicins
                );
            var subset = searchHits.Skip((page - 1) * pageSize).Take(pageSize);
            var count = searchHits.Count();
            var searchResult = new SearchResult<Medicin>()
            {
                ItemsList = new StaticPagedList<Medicin>(subset.Select(m => new Medicin()
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

                }), page, pageSize, count),
                SearchQuery = query
                ,
                CreateFrom = createFrom,
                CreateTo = createTo
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
