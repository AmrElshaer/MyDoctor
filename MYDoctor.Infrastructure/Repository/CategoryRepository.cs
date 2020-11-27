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
using MYDoctor.Infrastructure.Identity;

namespace MYDoctor.Infrastructure.Repository
{
    public class CategoryRepository:BaseRepository<BeatyandHealthy>,ICategoryRepository
    {
        
        
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {}
        public async Task<IEnumerable<GeneralSearchResult>> GeneralSearchAsync(string searchval) {
            var categories =await GetAll(c=>c.Category.ToLower().Contains(searchval)).Select(c=>new GeneralSearchResult{Controller="BeatyandHealthies",Action="Details",Id=c.Id,Image=c.Image,Title=c.Category}).ToListAsync();
            var relativeofBeatyandhealthies =await _context.RelativeofBeatyandhealthy.Where(c=>c.Address.ToLower().Contains(searchval)).Select(r=>new GeneralSearchResult() {Controller="RelativesCategory",Action="Details",Id=r.Id,Image=r.ImageOrVideo,Title=r.Address,Content=r.Subject.Substring(0,50) }).ToListAsync();
            var doctors =await _context.Doctor.Where(c=>c.Name.ToLower().Contains(searchval)).Select(d=>new GeneralSearchResult() { Controller="Doctor",Action="Profile",Image= $"/images/{d.ImagePath}",Title=d.Name,Id=d.Id,Content=d.Others.Substring(0, 50) }).ToListAsync();
            var medicins =await _context.Medicin.Where(c=>c.Name.ToLower().Contains(searchval)).Select(m=>new GeneralSearchResult() {Controller="Medicins",Image=m.Image,Title=m.Name,Action="Details",Id=m.Id,Content=m.Indications.Substring(0, 50) }).ToListAsync();
            var diseases =await _context.Disease.Where(c=>c.DiseaseName.ToLower().Contains(searchval)).Select(d=>new GeneralSearchResult() { Controller="Diseases",Action="Details",Id=d.Id,Image=d.Image,Title=d.DiseaseName,Content=d.Subject.Substring(0,50)}).ToListAsync();
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
            var subset = searchHits.Skip((searchParamter.Page.Value- 1) * searchParamter.PageSize).Take(searchParamter.PageSize);
             searchParamter.TotalCount= searchHits.Count();
            var searchResult = new SearchResult<BeatyandHealthy>()
            {
                ItemsList =subset,
                SearchParamter = searchParamter
            };
            return searchResult;
        }
        public async Task<BaseViewModel> GetCategoryAsync(int categoryId, int numberRelated)
        {

            var cateogry = await GetByIdAsync(categoryId, c => c.Diseases, c => c.Medicins, c => c.Doctors, c => c.RelativeofBeatyandhealthies);

            var result = new BeatyandHealthViewModel()
            {
                BeatyandHealthy = cateogry,
                Categories = await GetAll(a => a.Id != categoryId).ToListAsync(),
                Doctors = (cateogry.Doctors.Any(), cateogry.Doctors.Count() >= numberRelated) == (true, true) ?
                           cateogry.Doctors :
                           cateogry.Doctors.AppendData(await _context.Doctor.Include(d => d.Category).Where( d => d.Category.Id != categoryId).Take(numberRelated - cateogry.Doctors.Count()).ToListAsync()),
                Medicins = (cateogry.Medicins.Any(), cateogry.Medicins.Count() >= numberRelated) == (true, true) ?
                           cateogry.Medicins :
                           cateogry.Medicins.AppendData(await _context.Medicin.Include(d => d.BeatyandHealthy).Where( d => d.BeatyandHealthy.Id != categoryId).Take(numberRelated - cateogry.Medicins.Count()).ToListAsync()),
                Diseases = (cateogry.Diseases.Any(), cateogry.Diseases.Count() >= numberRelated) == (true, true) ?
                           cateogry.Diseases :
                           cateogry.Diseases.AppendData(await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != categoryId).Take(numberRelated - cateogry.Diseases.Count()).ToListAsync()),
                RelativeCategories = (cateogry.RelativeofBeatyandhealthies.Any(), cateogry.RelativeofBeatyandhealthies.Count() >= numberRelated) == (true, true) ?
                                     cateogry.RelativeofBeatyandhealthies :
                                      cateogry.RelativeofBeatyandhealthies.AppendData(await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != categoryId).Take(numberRelated - cateogry.RelativeofBeatyandhealthies.Count()).ToListAsync())
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
       
    }
}
