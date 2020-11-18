using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Data;
using MyDoctor.Helper;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
using MyDoctor.ViewModels;
using PagedList.Core;

namespace MyDoctor.Repository
{
    public class CategoryRepository:BaseRepository<BeatyandHealthy>,ICategoryRepository
    {
        
        
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {}

        public SearchResult<BeatyandHealthy> GetSearchResult(string query, int page, int pageSize,DateTime? createFrom,DateTime? createTo)
        {
            var searchHits = GetAll(x =>
                (query == null || x.Category.ToLower().Contains(query.ToLower()))
                && (createFrom == null || x.CreateDate >= createFrom)
                && (createTo == null || x.CreateDate <= createTo)
                , c => c.OrderByDescending(a => a.Id)
                );
            var subset = searchHits.Skip((page - 1) * pageSize).Take(pageSize);
            var count = searchHits.Count();
            var searchResult = new SearchResult<BeatyandHealthy>()
            {
                ItemsList =new StaticPagedList<BeatyandHealthy>(subset, page, pageSize, count),
                SearchQuery = query
                ,CreateFrom = createFrom,
                CreateTo = createTo
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
