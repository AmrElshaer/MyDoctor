using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Infrastructure.Identity;

namespace MYDoctor.Infrastructure.Repository
{
    public class CategoryRepository:BaseRepository<BeatyandHealthy>,ICategoryRepository
    {
        
        
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {}
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

            var cateogry = await GetByIdAsync(categoryId, c => c.Diseases, c => c.Medicins, c => c.Doctors, c => c.RelativeofBeatyandhealthies);

            var result = new BeatyandHealthViewModel()
            {
                BeatyandHealthy = cateogry,
                Categories = await GetAll(a => a.Id != categoryId).ToListAsync(),
                Doctors =await GetRelativesDoctors(cateogry,numberRelated),
                Medicins =await GetRelativesMedicins(cateogry,numberRelated),
                Diseases = await GetRelativesDiseases(cateogry,numberRelated),
                RelativeCategories =await GetRelativesCategories(cateogry,numberRelated)
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
                RelativeCategories = await _context.RelativeofBeatyandhealthy.Include(rc => rc.BeatyandHealthy).Take(pageSize).ToListAsync()
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
        private async Task<IEnumerable<Doctor>> GetRelativesDoctors(BeatyandHealthy category, int numberRelated)
        {
            var relativeDoctors = category.Doctors;
            if ((relativeDoctors.Any(), relativeDoctors.Count() >= numberRelated) == (true, true))
                return relativeDoctors;
            var doctors = await _context.Doctor.Include(d => d.Category).Where(d => d.CategoryId != category.Id).Take(numberRelated - relativeDoctors.Count()).ToListAsync();
            return relativeDoctors.AppendData(doctors);
        }
        private async Task<IEnumerable<Medicin>> GetRelativesMedicins(BeatyandHealthy category, int numberRelated)
        {
            var relativeMedicin = category.Medicins;
            if ((relativeMedicin.Any(), relativeMedicin.Count() >= numberRelated) == (true, true))
                return relativeMedicin;
            var medicins = await _context.Medicin.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != category.Id).Take(numberRelated - relativeMedicin.Count()).ToListAsync();
            return relativeMedicin.AppendData(medicins);
        }
        private async Task<IEnumerable<Disease>> GetRelativesDiseases(BeatyandHealthy category, int numberRelated)
        {
            var relativeDisease = category.Diseases;
            if ((relativeDisease.Any(), relativeDisease.Count() >= numberRelated) == (true, true))
                return relativeDisease;
            var medicins = await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != category.Id).Take(numberRelated - relativeDisease.Count()).ToListAsync();
            return relativeDisease.AppendData(medicins);
        }
        private async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategories(BeatyandHealthy category, int numberRelated)
        {
            var relativeCategories = category.RelativeofBeatyandhealthies;
            if ((relativeCategories.Any(), relativeCategories.Count() >= numberRelated) == (true, true))
                return relativeCategories;
            var medicins = await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthId != category.Id).Take(numberRelated - relativeCategories.Count()).ToListAsync();
            return relativeCategories.AppendData(medicins);
        }

    }
}
