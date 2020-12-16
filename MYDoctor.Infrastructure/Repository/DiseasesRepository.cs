
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Infrastructure.Identity;

namespace MYDoctor.Infrastructure.Repository
{
    public class DiseasesRepository:BaseRepository<Disease>,IDiseasesRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;
        private readonly IDoctorHelper _doctorHelper;
        private readonly IMedicinHelper _medicinHelper;
        private readonly IPostHelper _postHelper;
        private readonly IDiseaseHelper _diseaseHelper;
        private readonly IRelativeCategoryHelper _relativeCategoryHelper;
        public DiseasesRepository(ApplicationDbContext context, IDiseaseHelper diseaseHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper, IMedicinHelper medicinHelper, IDoctorHelper doctorHelper, ITableTrackNotification tableTrackNotification) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
            _doctorHelper = doctorHelper;
            _medicinHelper = medicinHelper;
            _postHelper = postHelper;
            _diseaseHelper = diseaseHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
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
                await _tableTrackNotification.InsertAsync(disease.DiseaseName, disease.Subject, "Diseases", "Details", disease.Image, disease.Id);

            }

        }

        public async Task<BaseViewModel> GetDiseaseAsync(int id, int numberRelated)
        {
            var disease = await _context.Disease.Include(m => m.BeatyandHealthy)
                  .ThenInclude(c => c.Doctors).Include(m => m.BeatyandHealthy.Medicins)
                  .Include(m => m.BeatyandHealthy.Posts).
                Include(m => m.BeatyandHealthy.RelativeofBeatyandhealthies)
                .Include(m => m.DiseaseMedicins).ThenInclude(dm => dm.Disease)
                .FirstOrDefaultAsync(m => m.Id == id);

            var result = new DiseaseViewModel()
            {
                Disease = disease,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != disease.BeatyandHealthyId).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Doctors = await _doctorHelper.GetRelativesDoctors(disease.BeatyandHealthy.Doctors,numberRelated,d=>d.CategoryId!=disease.BeatyandHealthyId),
                Medicins = await _medicinHelper.GetRelativesMedicins(disease.BeatyandHealthy.Medicins,numberRelated, d => d.BeatyandHealthyId != disease.BeatyandHealthyId),
                Diseases = await _diseaseHelper.GetRelativesDiseases(disease.BeatyandHealthy.Diseases,numberRelated, d => d.BeatyandHealthyId != disease.BeatyandHealthyId),
                RelativeCategories = await _relativeCategoryHelper.GetRelativesCategory(disease.BeatyandHealthy.RelativeofBeatyandhealthies,numberRelated, d => d.BeatyandHealthId != disease.BeatyandHealthyId),
                Posts=await _postHelper.GetRelativesPosts(disease.BeatyandHealthy.Posts,numberRelated,p=>p.CategoryId!=disease.BeatyandHealthyId)
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
            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
            return searchResult;
        }
      
    }
}
