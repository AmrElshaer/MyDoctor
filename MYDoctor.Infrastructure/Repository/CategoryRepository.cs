using DocumentFormat.OpenXml.Spreadsheet;
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class CategoryRepository : BaseRepository<BeatyandHealthy>, ICategoryRepository
    {
        private readonly IServiceBuilder serviceBuilder;

        public CategoryRepository(ApplicationDbContext context, IServiceBuilder serviceBuilder) : base(context)
        {
            this.serviceBuilder = serviceBuilder;
        }

        public async Task CreateEdit(BeatyandHealthy category)
        {
            if (category.Id != 0)
            {
                category.ModifiedDate = DateTime.Now.Date;
                await UpdateAsync(category);
            }
            else
            {
                category.CreateDate = DateTime.Now.Date;
                await InsertAsync(category);
            }
        }
        #region General Search

        public async Task<IEnumerable<GeneralSearchResult>> GeneralSearchAsync(string searchval)
        {
            return await SearchInAll(searchval);
        }

        private async Task<IEnumerable<GeneralSearchResult>> SearchInAll(string searchval)
        {
            var result = new List<GeneralSearchResult>();
            result.AddRange(await GetCategories(searchval));
            result.AddRange(await GetRelativeCategories(searchval));
            result.AddRange(await GetDoctors(searchval));
            result.AddRange(await GetMedicins(searchval));
            result.AddRange(await GetDiseases(searchval));
            return result;
        }

        private async Task<List<GeneralSearchResult>> GetDiseases(string searchval)
        {
            return await _context.Disease.Where(c => c.DiseaseName.ToLower().Contains(searchval))
                .Select(d => new GeneralSearchResult(d.Id, "Diseases", "Details", d.DiseaseName, d.Image, d.Subject.Substring(0, 50))).ToListAsync();
        }

        private async Task<List<GeneralSearchResult>> GetMedicins(string searchval)
        {
            return await _context.Medicin.Where(c => c.Name.ToLower().Contains(searchval))
                .Select(m => new GeneralSearchResult(m.Id, "Medicins", "Details", m.Name, m.Image,
                m.Indications.Substring(0, 50))).ToListAsync();
        }

        private async Task<List<GeneralSearchResult>> GetDoctors(string searchval)
        {
            return await _context.Doctor.Where(c => c.Name.ToLower().Contains(searchval))
                .Select(d => new GeneralSearchResult(d.Id, "Doctor", "Profile", d.Name, $"/images/{d.ImagePath}"
                , d.Others.Substring(0, 50))).ToListAsync();
        }

        private async Task<List<GeneralSearchResult>> GetRelativeCategories(string searchval)
        {
            return await _context.RelativeofBeatyandhealthy
                .Where(c => c.Address.ToLower().Contains(searchval))
                .Select(r => new GeneralSearchResult(r.Id, "RelativesCategory", "Details",
                r.Address, r.ImageOrVideo, r.Subject.Substring(0, 50))).ToListAsync();
        }

        private async Task<List<GeneralSearchResult>> GetCategories(string searchval)
        {
            return await GetAll(c => c.Category.ToLower().Contains(searchval))
                            .Select(c => new GeneralSearchResult(c.Id, "BeatyandHealthies", "Details", c.Category, c.Image, null)).ToListAsync();
        }
        #endregion 
        public async Task<IEnumerable<BeatyandHealthy>> GetAdminBoard()
        {
            var board = await GetAll().Include(c => c.RelativeofBeatyandhealthies)
                .Include(c => c.Medicins).Include(c => c.Diseases).Include(c => c.Doctors)
                .Include(c => c.Posts).ThenInclude(p => p.User).ToListAsync();
            return board;
        }
        public BaseViewModel<BeatyandHealthy> GetBoardViewModel(int pageSize)
        {
            return serviceBuilder.BuildViewModel(new DashBoardViewModel(pageSize));
        }

        public async Task<BaseViewModel<BeatyandHealthy>> GetCategoryAsync(int categoryId, int numberRelated)
        {
            BeatyandHealthy cateogry = await GetCategory(categoryId);
            return serviceBuilder.BuildViewModel(new BeatyandHealthViewModel(cateogry, numberRelated));
        }

        private async Task<BeatyandHealthy> GetCategory(int categoryId)
        {
            return await GetByIdAsync(categoryId, c => c.Diseases, c => c.Medicins,c => c.Doctors, c => c.RelativeofBeatyandhealthies, c => c.Posts);
        }

        public async Task<IEnumerable<BeatyandHealthy>> GetSearchHits(SearchParamter searchParamter)
        {
            return await Search(searchParamter).ToListAsync();
        }

        public SearchResult<BeatyandHealthy> GetSearchResult(SearchParamter searchParamter)
        {
            return PagingHelper.PagingModel(Search(searchParamter), searchParamter);
        }

        private IQueryable<BeatyandHealthy> Search(SearchParamter searchParamter)
        {
            return GetAll(ApplyMainFilter(searchParamter)).OrderByDescending(a=>a.Id);
        }
        private Expression<Func<BeatyandHealthy,bool>> ApplyMainFilter(SearchParamter filter)
        {
            return e=>(string.IsNullOrEmpty(filter.SearchQuery) ||
              e.Category.ToLower().Contains(filter.SearchQuery.ToLower()))
              && (!filter.CreateFrom.HasValue || e.CreateDate >= filter.CreateFrom)
              && (!filter.CreateTo.HasValue || e.CreateDate <= filter.CreateTo);


        }
    }
}