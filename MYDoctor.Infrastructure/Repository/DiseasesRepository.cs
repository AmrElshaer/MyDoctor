
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;

namespace MYDoctor.Infrastructure.Repository
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

        public async Task<BaseViewModel> GetDiseaseAsync(int id, int numberRelated)
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

        public SearchResult<Disease> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(x =>
                (string.IsNullOrEmpty(searchParamter.SearchQuery)|| x.DiseaseName.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
                && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo)
                , d => d.OrderByDescending(a => a.Id)
                , d=>d.BeatyandHealthy);
            var subset = searchHits.Skip((searchParamter.Page.Value - 1) * searchParamter.PageSize).Take(searchParamter.PageSize);
            searchParamter.TotalCount= searchHits.Count();
            var searchResult = new SearchResult<Disease>()
            {
                ItemsList = subset,
                SearchParamter=searchParamter
            };
            return searchResult;
        }
       
    }
}
