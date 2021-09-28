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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class CategoryRelativiesRepository : BaseRepository<RelativeofBeatyandhealthy>, ICategoryRelativiesRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;
        private readonly IServiceBuilder serviceBuilder;

        public CategoryRelativiesRepository(ApplicationDbContext context,IServiceBuilder serviceBuilder, ITableTrackNotification tableTrackNotification) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
            this.serviceBuilder = serviceBuilder;
        }

        public SearchResult<RelativeofBeatyandhealthy> GetSearchResult(SearchParamter searchParamter)
        {
            return PagingHelper.PagingModel(Search(searchParamter), searchParamter);
        }
        public async Task<IEnumerable<RelativeofBeatyandhealthy> >SearchHits(SearchParamter searchParamter)
        {
            return await Search(searchParamter).ToListAsync();
        }
        private IQueryable<RelativeofBeatyandhealthy> Search(SearchParamter searchParamter)
        {
            return GetAll(ApplyFiliter(searchParamter)).Include(rc => rc.BeatyandHealthy).OrderByDescending(a => a.Id);
        }

        private  Expression<Func<RelativeofBeatyandhealthy, bool>> ApplyFiliter(SearchParamter searchParamter)
        {
            return
                             x =>
                             (string.IsNullOrEmpty(searchParamter.SearchQuery) ||
                             x.Address.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                             && (!searchParamter.CreateFrom.HasValue || x.CreateDate >= searchParamter.CreateFrom)
                             && (!searchParamter.CreateTo.HasValue || x.CreateDate <= searchParamter.CreateTo)
                             && (!searchParamter.IdRelated.HasValue || x.BeatyandHealthy.Id == searchParamter.IdRelated);
        }

        public async Task CreateEdit(RelativeofBeatyandhealthy category)
        {
            if (category.Id != 0)
            {
                category.ModiteDate = DateTime.Now.Date;
                await UpdateAsync(category);
            }
            else
            {
                category.CreateDate = DateTime.Now.Date;
                await InsertAsync(category);
                await _tableTrackNotification.InsertAsync(category.Address, category.Subject, "RelativesCategory", "Details", category.ImageOrVideo, category.Id);
            }
        }

        public async Task<BaseViewModel<RelativeofBeatyandhealthy>> GetRelativeCategoryAsync(int id, int numberRelated)
        {
            
            return this.serviceBuilder.BuildViewModel(
                new RelativeBeatyandhealthyViewModel(await GetRelatCategory(id), numberRelated)
                );

        }

        private async Task<RelativeofBeatyandhealthy> GetRelatCategory(int id)
        {
            return await GetAll().Include(r => r.BeatyandHealthy)
                .Include(r => r.BeatyandHealthy.Doctors).Include(r => r.BeatyandHealthy.Medicins)
                .Include(r => r.BeatyandHealthy.Diseases).Include(r => r.BeatyandHealthy.RelativeofBeatyandhealthies)
                .Include(c => c.BeatyandHealthy.Posts).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}