
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
    public class DiseasesRepository:BaseRepository<Disease>,IDiseasesRepository
    {
        public DiseasesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateEdit(Disease disease)
        {
            if (disease.Id > 0)
            {
                disease.ModifiedDate = DateTime.Now;
                await UpdateAsync(disease);
            }
            else
            {
                disease.CreateDate = DateTime.Now;
                await InsertAsync(disease);
            }
            
        }

        public async Task<DiseaseViewModel> GetDiseaseAsync(int id, int numberRelated)
        {
            var disease = await _context.Disease.Include(m => m.BeatyandHealthy).Include(m => m.DiseaseMedicins).ThenInclude(dm => dm.Disease).FirstOrDefaultAsync(m => m.Id == id);

            var result = new DiseaseViewModel()
            {
                Disease = disease,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != disease.BeatyandHealthyId).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Doctors = (disease.BeatyandHealthy.Doctors.Any(), disease.BeatyandHealthy.Doctors.Count() >= numberRelated) == (true, true) ?
                           disease.BeatyandHealthy.Doctors :
                           disease.BeatyandHealthy.Doctors.AppendData(await _context.Doctor.Include(d => d.Category).Where(d => d.Category.Id != disease.BeatyandHealthyId).Take(numberRelated - disease.BeatyandHealthy.Doctors.Count()).ToListAsync()),
                Medicins = await _context.Medicin.Where(m => m.Id != id).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Diseases = (disease.BeatyandHealthy.Diseases.Any(), disease.BeatyandHealthy.Diseases.Count() >= numberRelated) == (true, true) ?
                           disease.BeatyandHealthy.Diseases :
                           disease.BeatyandHealthy.Diseases.AppendData(await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != disease.BeatyandHealthyId).Take(numberRelated - disease.BeatyandHealthy.Diseases.Count()).ToListAsync()),
                RelativeCategories = (disease.BeatyandHealthy.RelativeofBeatyandhealthies.Any(), disease.BeatyandHealthy.RelativeofBeatyandhealthies.Count() >= numberRelated) == (true, true) ?
                                     disease.BeatyandHealthy.RelativeofBeatyandhealthies :
                                      disease.BeatyandHealthy.RelativeofBeatyandhealthies.AppendData(await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != disease.BeatyandHealthyId).Take(numberRelated - disease.BeatyandHealthy.RelativeofBeatyandhealthies.Count()).ToListAsync())
            };
            return result;
        }

        public SearchResult<Disease> GetSearchResult(string query, int pageNumber, int pageSize, DateTime? createFrom, DateTime? createTo)
        {
            var searchHits = GetAll(x =>
                (query == null || x.DiseaseName.ToLower().Contains(query.ToLower()))
                && (createFrom == null || x.CreateDate >= createFrom)
                && (createTo == null || x.CreateDate <= createTo)
                , d => d.OrderByDescending(a => a.Id)
                , 
                    d=>d.BeatyandHealthy
                
                );
            var subset = searchHits.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var count = searchHits.Count();
            var searchResult = new SearchResult<Disease>()
            {
                ItemsList = new StaticPagedList<Disease>(subset, pageNumber, pageSize, count),
                SearchQuery = query
                ,
                CreateFrom = createFrom,
                CreateTo = createTo
            };
            return searchResult;
        }
       
    }
}
