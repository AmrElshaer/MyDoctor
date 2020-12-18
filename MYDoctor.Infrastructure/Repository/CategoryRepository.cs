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
    public class CategoryRepository:BaseRepository<BeatyandHealthy>,ICategoryRepository
    {
        private readonly IDoctorHelper _doctorHelper;
        private readonly IDiseaseHelper _diseaseHelper;
        private readonly IPostHelper _postHelper;
        private readonly IMedicinHelper _medicinHelper;
        private readonly IRelativeCategoryHelper _relativeCategoryHelper;

        public CategoryRepository(ApplicationDbContext context, IMedicinHelper medicinHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper, IDiseaseHelper diseaseHelper, IDoctorHelper doctorHelper) : base(context)
        {
            _doctorHelper = doctorHelper;
            _diseaseHelper = diseaseHelper;
            _postHelper = postHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
            _medicinHelper = medicinHelper;
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
        public SearchResult<BeatyandHealthy> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(x =>
                (string.IsNullOrEmpty(searchParamter.SearchQuery)|| x.Category.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                && (!searchParamter.CreateFrom.HasValue || x.CreateDate >= searchParamter.CreateFrom)
                && (!searchParamter.CreateTo.HasValue|| x.CreateDate <= searchParamter.CreateTo)
                , c => c.OrderByDescending(a => a.Id)
                );
            var searchResult = PagingHelper.PagingModel(searchHits,searchParamter);
            return searchResult;
        }
        public async Task<BaseViewModel> GetCategoryAsync(int categoryId, int numberRelated)
        {

            var cateogry = await GetByIdAsync(categoryId, c => c.Diseases, c => c.Medicins, c => c.Doctors, c => c.RelativeofBeatyandhealthies,c=>c.Posts);

            var result = new BeatyandHealthViewModel()
            {
                BeatyandHealthy = cateogry,
                Categories = await GetAll(a => a.Id != categoryId).ToListAsync(),
                Doctors = await _doctorHelper.GetRelativesDoctors(cateogry.Doctors, numberRelated, d => d.CategoryId != cateogry.Id),
                Medicins = await _medicinHelper.GetRelativesMedicins(cateogry.Medicins, numberRelated, d => d.BeatyandHealthyId != cateogry.Id),
                Diseases = await _diseaseHelper.GetRelativesDiseases(cateogry.Diseases, numberRelated, d => d.BeatyandHealthyId != cateogry.Id),
                RelativeCategories = await _relativeCategoryHelper.GetRelativesCategory(cateogry.RelativeofBeatyandhealthies, numberRelated, d => d.BeatyandHealthId != cateogry.Id),
                Posts = await _postHelper.GetRelativesPosts(cateogry.Posts, numberRelated, p => p.CategoryId != cateogry.Id)
            };
            return result;
        }
        public async Task<BaseViewModel> GetBoardViewModel(int pageSize)
        {
            var result = new DashBoardViewModel()
            {
                Categories = await GetAll().Take(pageSize).ToListAsync(),
                Doctors = await _context.Doctor.Include(rc => rc.Category).Take(pageSize).ToListAsync(),
                Medicins = await _context.Medicin.Include(rc => rc.BeatyandHealthy).Take(pageSize).ToListAsync(),
                Diseases = await _context.Disease.Include(rc => rc.BeatyandHealthy).Take(pageSize).ToListAsync(),
                RelativeCategories = await _context.RelativeofBeatyandhealthy.Include(rc => rc.BeatyandHealthy).Take(pageSize).ToListAsync(),
                Posts=await _context.Posts.Include(p=>p.Likes).Include(p=>p.DisLikes).Include(p=>p.Category).Include(p=>p.User).OrderByDescending(a=>a.Id).Take(pageSize).ToListAsync()
            };
            return result;
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
    }
}
