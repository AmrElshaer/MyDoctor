using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class DiseasesRepository : BaseRepository<Disease>, IDiseasesRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;
        private readonly IServiceBuilder serviceBuilder;

        public DiseasesRepository(ApplicationDbContext context, IServiceBuilder serviceBuilder, ITableTrackNotification tableTrackNotification) : base(context)
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
            return serviceBuilder.BuildViewModel(new DiseaseViewModel(await GetDisease(id), numberRelated));
        }

        private async Task<Disease> GetDisease(int id)
        {
            return await _context.Disease.Include(m => m.BeatyandHealthy)
                              .ThenInclude(c => c.Doctors).Include(m => m.BeatyandHealthy.Medicins)
                              .Include(m => m.BeatyandHealthy.Posts).
                            Include(m => m.BeatyandHealthy.RelativeofBeatyandhealthies)
                            .Include(m => m.DiseaseMedicins).ThenInclude(dm => dm.Disease)
                            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public SearchResult<Disease> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(ApplyFiliter(searchParamter)).Include(d => d.BeatyandHealthy).OrderByDescending(a => a.Id);
            return PagingHelper.PagingModel(searchHits, searchParamter);
        }

        private  Expression<Func<Disease, bool>> ApplyFiliter(SearchParamter searchParamter)
        {
            return x =>
                           (string.IsNullOrEmpty(searchParamter.SearchQuery) || x.DiseaseName.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                           && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
                           && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo);
        }
    }
}