using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
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

        
    }
}
