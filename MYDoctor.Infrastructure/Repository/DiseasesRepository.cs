
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application;
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
        private readonly IServiceBuilder serviceBuilder;
        public DiseasesRepository(ApplicationDbContext context,IServiceBuilder serviceBuilder ,ITableTrackNotification tableTrackNotification) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
            this.serviceBuilder = serviceBuilder;
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

        public async Task<BaseViewModel<Disease>> GetDiseaseAsync(int id, int numberRelated)
        {
            var disease = await _context.Disease.Include(m => m.BeatyandHealthy)
                  .ThenInclude(c => c.Doctors).Include(m => m.BeatyandHealthy.Medicins)
                  .Include(m => m.BeatyandHealthy.Posts).
                Include(m => m.BeatyandHealthy.RelativeofBeatyandhealthies)
                .Include(m => m.DiseaseMedicins).ThenInclude(dm => dm.Disease)
                .FirstOrDefaultAsync(m => m.Id == id);
            return serviceBuilder.BuildViewModel(new DiseaseViewModel(disease.BeatyandHealthyId.Value, 
                disease,numberRelated));
          
        }

        public SearchResult<Disease> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(x =>
               (string.IsNullOrEmpty(searchParamter.SearchQuery) || x.DiseaseName.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
               && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
               && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo)
               , d => d.OrderByDescending(a => a.Id)
               , d => d.BeatyandHealthy);
            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
            return searchResult;
        }
      
    }
}
