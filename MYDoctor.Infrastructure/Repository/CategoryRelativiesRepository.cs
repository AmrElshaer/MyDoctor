using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using MYDoctor.Core.Application.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class CategoryRelativiesRepository:BaseRepository<RelativeofBeatyandhealthy>,ICategoryRelativiesRepository
    {
        public CategoryRelativiesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SearchResult<RelativeofBeatyandhealthy> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(
                x =>
                (string.IsNullOrEmpty(searchParamter.SearchQuery) || x.Address.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                && (!searchParamter.CreateFrom.HasValue|| x.CreateDate >= searchParamter.CreateFrom)
                && (!searchParamter.CreateTo.HasValue|| x.CreateDate <= searchParamter.CreateTo)
                && (!searchParamter.IdRelated.HasValue || x.BeatyandHealthy.Id == searchParamter.IdRelated),
                rc => rc.OrderByDescending(a => a.Id),
                rc=>rc.BeatyandHealthy
                
                );
            var subset = searchHits.Skip((searchParamter.Page.Value - 1) * searchParamter.PageSize).Take(searchParamter.PageSize);
            searchParamter.TotalCount= searchHits.Count();
            var searchResult = new SearchResult<RelativeofBeatyandhealthy>()
            {
                ItemsList = subset,
                SearchParamter=searchParamter
            };
            return searchResult;
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


            }



        }

        public async Task<RelativeBeatyandhealthyViewModel> GetRelativeCategoryAsync(int id, int numberRelated)
        {
            var relativeCategory = await _table.Include(r=>r.BeatyandHealthy).Include(r=>r.BeatyandHealthy.Doctors).Include(r=>r.BeatyandHealthy.Medicins)
                .Include(r=>r.BeatyandHealthy.Diseases).Include(r=>r.BeatyandHealthy.RelativeofBeatyandhealthies).AsNoTracking().FirstOrDefaultAsync(r=>r.Id==id);

            var result = new RelativeBeatyandhealthyViewModel()
            {
                RelativeofBeatyandhealthy = relativeCategory,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != relativeCategory.BeatyandHealthy.Id).ToListAsync(),
                Doctors = (relativeCategory.BeatyandHealthy.Doctors.Any(), relativeCategory.BeatyandHealthy.Doctors.Count() >= numberRelated) == (true, true) ?
                           relativeCategory.BeatyandHealthy.Doctors :
                           relativeCategory.BeatyandHealthy.Doctors.AppendData(await _context.Doctor.Include(d => d.Category).Where(d => d.Category.Id != id).Take(numberRelated - relativeCategory.BeatyandHealthy.Doctors.Count()).ToListAsync()),
                Medicins = (relativeCategory.BeatyandHealthy.Medicins.Any(), relativeCategory.BeatyandHealthy.Medicins.Count() >= numberRelated) == (true, true) ?
                           relativeCategory.BeatyandHealthy.Medicins :
                           relativeCategory.BeatyandHealthy.Medicins.AppendData(await _context.Medicin.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != id).Take(numberRelated - relativeCategory.BeatyandHealthy.Medicins.Count()).ToListAsync()),
                Diseases = (relativeCategory.BeatyandHealthy.Diseases.Any(), relativeCategory.BeatyandHealthy.Diseases.Count() >= numberRelated) == (true, true) ?
                           relativeCategory.BeatyandHealthy.Diseases :
                           relativeCategory.BeatyandHealthy.Diseases.AppendData(await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != id).Take(numberRelated - relativeCategory.BeatyandHealthy.Diseases.Count()).ToListAsync()),
                RelativeCategories = (relativeCategory.BeatyandHealthy.RelativeofBeatyandhealthies.Any(), relativeCategory.BeatyandHealthy.RelativeofBeatyandhealthies.Count() >= numberRelated) == (true, true) ?
                                     relativeCategory.BeatyandHealthy.RelativeofBeatyandhealthies :
                                      relativeCategory.BeatyandHealthy.RelativeofBeatyandhealthies.AppendData(await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthy.Id != id).Take(numberRelated - relativeCategory.BeatyandHealthy.RelativeofBeatyandhealthies.Count()).ToListAsync())
            };
            return result;
        }
    }
}
