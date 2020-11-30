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
using MYDoctor.Infrastructure.Helper;
using System.Collections.Generic;

namespace MYDoctor.Infrastructure.Repository
{
    public class CategoryRelativiesRepository:BaseRepository<RelativeofBeatyandhealthy>,ICategoryRelativiesRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;

        public CategoryRelativiesRepository(ApplicationDbContext context,ITableTrackNotification tableTrackNotification) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
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
          
            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
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
                await _tableTrackNotification.InsertAsync(category.Address, category.Subject, "RelativesCategory", "Details", category.ImageOrVideo, category.Id);
            }



        }

        public async Task<RelativeBeatyandhealthyViewModel> GetRelativeCategoryAsync(int id, int numberRelated)
        {
            var relativeCategory = await _table.Include(r=>r.BeatyandHealthy).Include(r=>r.BeatyandHealthy.Doctors).Include(r=>r.BeatyandHealthy.Medicins)
                .Include(r=>r.BeatyandHealthy.Diseases).Include(r=>r.BeatyandHealthy.RelativeofBeatyandhealthies).AsNoTracking().FirstOrDefaultAsync(r=>r.Id==id);

            var result = new RelativeBeatyandhealthyViewModel()
            {
                RelativeofBeatyandhealthy = relativeCategory,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != relativeCategory.BeatyandHealthy.Id).Take(numberRelated).ToListAsync(),
                Doctors = await GetRelativesDoctors(relativeCategory,numberRelated),
                Medicins = await GetRelativesMedicins(relativeCategory,numberRelated),
                Diseases =await GetRelativesDiseases(relativeCategory,numberRelated),
                RelativeCategories = await GetRelativesCategories(relativeCategory,numberRelated)
            };
            return result;
        }
        private async Task<IEnumerable<Doctor>> GetRelativesDoctors(RelativeofBeatyandhealthy relativeCategory, int numberRelated) {
            var relativeDoctors = relativeCategory.BeatyandHealthy.Doctors;
            if ((relativeDoctors.Any(), relativeDoctors.Count() >= numberRelated) == (true, true))
                  return relativeDoctors;
            var doctors = await _context.Doctor.Include(d => d.Category).Where(d => d.CategoryId != relativeCategory.BeatyandHealthId).Take(numberRelated - relativeDoctors.Count()).ToListAsync();
            return relativeDoctors.AppendData(doctors);
        }
        private async Task<IEnumerable<Medicin>> GetRelativesMedicins(RelativeofBeatyandhealthy relativeCategory, int numberRelated)
        {
            var relativeMedicin = relativeCategory.BeatyandHealthy.Medicins;
            if ((relativeMedicin.Any(), relativeMedicin.Count() >= numberRelated) == (true, true))
                return relativeMedicin;
            var medicins = await _context.Medicin.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != relativeCategory.BeatyandHealthId).Take(numberRelated - relativeMedicin.Count()).ToListAsync();
            return relativeMedicin.AppendData(medicins);
        }
        private async Task<IEnumerable<Disease>> GetRelativesDiseases(RelativeofBeatyandhealthy relativeCategory, int numberRelated)
        {
            var relativeDisease = relativeCategory.BeatyandHealthy.Diseases;
            if ((relativeDisease.Any(), relativeDisease.Count() >= numberRelated) == (true, true))
                return relativeDisease;
            var medicins = await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != relativeCategory.BeatyandHealthId).Take(numberRelated - relativeDisease.Count()).ToListAsync();
            return relativeDisease.AppendData(medicins);
        }
        private async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategories(RelativeofBeatyandhealthy relativeCategory, int numberRelated)
        {
            var relativeCategories = relativeCategory.BeatyandHealthy.RelativeofBeatyandhealthies;
            if ((relativeCategories.Any(), relativeCategories.Count() >= numberRelated) == (true, true))
                return relativeCategories;
            var medicins = await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthId != relativeCategory.BeatyandHealthId).Take(numberRelated - relativeCategories.Count()).ToListAsync();
            return relativeCategories.AppendData(medicins);
        }
    }
}
