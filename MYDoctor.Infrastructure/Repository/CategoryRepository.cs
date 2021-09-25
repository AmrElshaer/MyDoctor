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
    public class CategoryRepository:BaseRepository<BeatyandHealthy>,ICategoryRepository
    {
      
        private readonly IServiceBuilder serviceBuilder;

        public CategoryRepository(ApplicationDbContext context,IServiceBuilder serviceBuilder) : base(context)
        {
          
            this.serviceBuilder = serviceBuilder;
          
        }
        public async Task<IEnumerable<BeatyandHealthy>> GetAdminBoard() {
            var board = await _context.BeatyandHealthy.Include(c=>c.RelativeofBeatyandhealthies)
                .Include(c=>c.Medicins).Include(c=>c.Diseases).Include(c=>c.Doctors)
                .Include(c=>c.Posts).ThenInclude(p=>p.User).ToListAsync();
            return board;
        
        }
        public async Task<IEnumerable<GeneralSearchResult>> GeneralSearchAsync(string searchval) {
            var categories = await GetAll(c => c.Category.ToLower().Contains(searchval)).Select(c => new GeneralSearchResult(c.Id, "BeatyandHealthies", "Details", c.Category, c.Image,null)).ToListAsync();
            var relativeofBeatyandhealthies =await _context.RelativeofBeatyandhealthy.Where(c=>c.Address.ToLower().Contains(searchval)).Select(r=>new GeneralSearchResult(r.Id,"RelativesCategory","Details", r.Address,r.ImageOrVideo,r.Subject.Substring(0,50) )).ToListAsync();
            var doctors =await _context.Doctor.Where(c=>c.Name.ToLower().Contains(searchval)).Select(d=>new GeneralSearchResult( d.Id,"Doctor","Profile",d.Name,$"/images/{d.ImagePath}",d.Others.Substring(0, 50))).ToListAsync();
            var medicins =await _context.Medicin.Where(c=>c.Name.ToLower().Contains(searchval)).Select(m=>new GeneralSearchResult(m.Id,"Medicins", "Details", m.Name,m.Image,m.Indications.Substring(0, 50))).ToListAsync();
            var diseases =await _context.Disease.Where(c=>c.DiseaseName.ToLower().Contains(searchval)).Select(d=>new GeneralSearchResult(d.Id,"Diseases","Details",d.DiseaseName,d.Image,d.Subject.Substring(0,50))).ToListAsync();
            var resuilt = new List<GeneralSearchResult>();
            resuilt.AddRange(categories);
            resuilt.AddRange(doctors);
            resuilt.AddRange(medicins);
            resuilt.AddRange(diseases);
            resuilt.AddRange(relativeofBeatyandhealthies);
            return resuilt;

        }
        private IQueryable<BeatyandHealthy> Search(SearchParamter searchParamter)
        {
            var searchHits = GetAll(x =>
              (string.IsNullOrEmpty(searchParamter.SearchQuery) ||
              x.Category.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
              && (!searchParamter.CreateFrom.HasValue || x.CreateDate >= searchParamter.CreateFrom)
              && (!searchParamter.CreateTo.HasValue || x.CreateDate <= searchParamter.CreateTo)
              , c => c.OrderByDescending(a => a.Id)
              );
            return searchHits;
        }
        public SearchResult<BeatyandHealthy> GetSearchResult(SearchParamter searchParamter)
        {
           return PagingHelper.PagingModel(Search(searchParamter), searchParamter);
            
        }

        
       

        public async Task<BaseViewModel<BeatyandHealthy>> GetCategoryAsync(int categoryId, int numberRelated)
        {

            var cateogry = await GetByIdAsync(categoryId, c => c.Diseases,
                c => c.Medicins,
                c => c.Doctors,
                c => c.RelativeofBeatyandhealthies,
                c=>c.Posts);
            return serviceBuilder.BuildViewModel( new BeatyandHealthViewModel(cateogry, numberRelated,categoryId));
        }
        public  BaseViewModel<BeatyandHealthy> GetBoardViewModel(int pageSize)
        {
            return serviceBuilder.BuildViewModel(new DashBoardViewModel(pageSize));
        }
        public async Task CreateEdit(BeatyandHealthy category)
        {
            if (category.Id != 0)
            {
                
                    category.ModifiedDate=DateTime.Now.Date;
                    await UpdateAsync(category);
               
            }
            else
            {
                category.CreateDate=DateTime.Now.Date;
                await InsertAsync(category);
               
               
            }

            

        }

        public async Task<IEnumerable<BeatyandHealthy>> GetSearchHits(SearchParamter searchParamter)
        {
            return await Search(searchParamter).ToListAsync();
        }
       
    }
}
