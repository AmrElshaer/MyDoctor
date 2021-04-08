
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
        private readonly ICategoryHelper _categoryHelper;

        public DiseasesRepository(ApplicationDbContext context, IDiseaseHelper diseaseHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper, IMedicinHelper medicinHelper, IDoctorHelper doctorHelper, ITableTrackNotification tableTrackNotification, ICategoryHelper categoryHelper) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
            _doctorHelper = doctorHelper;
            _medicinHelper = medicinHelper;
            _postHelper = postHelper;
            _diseaseHelper = diseaseHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
            _categoryHelper = categoryHelper;
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
            var categoryId = disease.BeatyandHealthyId.Value;
            var result = new DiseaseViewModel(disease, numberRelated)
                .WithMedicin(this._medicinHelper, categoryId)
                .WithRelativeCategory(this._relativeCategoryHelper, categoryId)
                .WithDisease(this._diseaseHelper, categoryId)
                .WithDoctors(this._doctorHelper, categoryId)
                .WithPosts(this._postHelper, categoryId)
                .WithCategories(this._categoryHelper, categoryId).Build();
            return result;
        }

        public SearchResult<Disease> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHint = new DiseaseSearchHint(searchParamter);
            var searchHits = GetAll(searchHint.ToExpression() , d => d.OrderByDescending(a => a.Id) 
            , d=>d.BeatyandHealthy);
            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
            return searchResult;
        }
      
    }
}
