using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class CategoryRelativiesRepository : BaseRepository<RelativeofBeatyandhealthy>, ICategoryRelativiesRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;
        private readonly IDoctorHelper _doctorHelper;
        private readonly IDiseaseHelper _diseaseHelper;
        private readonly IPostHelper _postHelper;
        private readonly ICategoryHelper _categoryHelper;
        private readonly IMedicinHelper _medicinHelper;
        private readonly IRelativeCategoryHelper _relativeCategoryHelper;

        public CategoryRelativiesRepository(ApplicationDbContext context,ICategoryHelper categoryHelper ,IMedicinHelper medicinHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper, IDiseaseHelper diseaseHelper, IDoctorHelper doctorHelper, ITableTrackNotification tableTrackNotification) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
            _doctorHelper = doctorHelper;
            _diseaseHelper = diseaseHelper;
            _postHelper = postHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
            _categoryHelper = categoryHelper;
            _medicinHelper = medicinHelper;
        }

        public SearchResult<RelativeofBeatyandhealthy> GetSearchResult(SearchParamter searchParamter)
        {
            IQueryable<RelativeofBeatyandhealthy> searchHits = SearchHits(searchParamter);

            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
            return searchResult;
        }

        public IQueryable<RelativeofBeatyandhealthy> SearchHits(SearchParamter searchParamter)
        {
            var searchExpresion = new CategoryRelativeSearchHint(searchParamter); 
            return GetAll(searchExpresion.ToExpression(), rc => rc.OrderByDescending(a => a.Id), rc => rc.BeatyandHealthy);
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

        public async Task<RelativeBeatyandhealthyViewModel> GetRelativeCategoryAsync(int id, int numberRelated)
        {
            var relativeCategory = await _table.Include(r => r.BeatyandHealthy)
                .Include(r => r.BeatyandHealthy.Doctors).Include(r => r.BeatyandHealthy.Medicins)
                .Include(r => r.BeatyandHealthy.Diseases).Include(r => r.BeatyandHealthy.RelativeofBeatyandhealthies)
                .Include(c => c.BeatyandHealthy.Posts).FirstOrDefaultAsync(r => r.Id == id);
            var result = new RelativeBeatyandhealthyViewModel(relativeCategory, numberRelated)
                .WithMedicin(this._medicinHelper, relativeCategory.BeatyandHealthId)
                .WithRelativeCategory(this._relativeCategoryHelper, relativeCategory.BeatyandHealthId)
                .WithDisease(this._diseaseHelper,  relativeCategory.BeatyandHealthId)
                .WithDoctors(this._doctorHelper,  relativeCategory.BeatyandHealthId)
                .WithPosts(this._postHelper,relativeCategory.BeatyandHealthId)
                .WithCategories(this._categoryHelper, relativeCategory.BeatyandHealthId).Build();
            return result;
        }
    }
}