
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
    public class DiseasesRepository:BaseRepository<Disease>,IDiseasesRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;

        public DiseasesRepository(ApplicationDbContext context,ITableTrackNotification tableTrackNotification) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
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
            var disease = await _context.Disease.Include(m => m.BeatyandHealthy).Include(m => m.DiseaseMedicins).ThenInclude(dm => dm.Disease).FirstOrDefaultAsync(m => m.Id == id);

            var result = new DiseaseViewModel()
            {
                Disease = disease,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != disease.BeatyandHealthyId).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Doctors = await GetRelativesDoctors(disease,numberRelated),
                Medicins = await GetRelativesMedicins(disease,numberRelated),
                Diseases = await GetRelativesDiseases(disease,numberRelated),
                RelativeCategories = await GetRelativesCategories(disease,numberRelated)
            };
            return result;
        }

        public SearchResult<Disease> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(x =>
                (string.IsNullOrEmpty(searchParamter.SearchQuery)|| x.DiseaseName.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
                && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo)
                , d => d.OrderByDescending(a => a.Id)
                , d=>d.BeatyandHealthy);
            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
            return searchResult;
        }
        private async Task<IEnumerable<Doctor>> GetRelativesDoctors(Disease disease, int numberRelated)
        {
            var relativeDoctors = disease.BeatyandHealthy.Doctors;
            if ((relativeDoctors.Any(), relativeDoctors.Count() >= numberRelated) == (true, true))
                return relativeDoctors;
            var doctors = await _context.Doctor.Include(d => d.Category).Where(d => d.CategoryId != disease.BeatyandHealthyId).Take(numberRelated - relativeDoctors.Count()).ToListAsync();
            return relativeDoctors.AppendData(doctors);
        }
        private async Task<IEnumerable<Medicin>> GetRelativesMedicins(Disease disease, int numberRelated)
        {
            var relativeMedicin = disease.BeatyandHealthy.Medicins;
            if ((relativeMedicin.Any(), relativeMedicin.Count() >= numberRelated) == (true, true))
                return relativeMedicin;
            var medicins = await _context.Medicin.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != disease.BeatyandHealthyId).Take(numberRelated - relativeMedicin.Count()).ToListAsync();
            return relativeMedicin.AppendData(medicins);
        }
        private async Task<IEnumerable<Disease>> GetRelativesDiseases(Disease disease, int numberRelated)
        {
            var relativeDisease = disease.BeatyandHealthy.Diseases;
            if ((relativeDisease.Any(), relativeDisease.Count() >= numberRelated) == (true, true))
                return relativeDisease;
            var medicins = await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != disease.BeatyandHealthyId).Take(numberRelated - relativeDisease.Count()).ToListAsync();
            return relativeDisease.AppendData(medicins);
        }
        private async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategories(Disease disease, int numberRelated)
        {
            var relativeCategories = disease.BeatyandHealthy.RelativeofBeatyandhealthies;
            if ((relativeCategories.Any(), relativeCategories.Count() >= numberRelated) == (true, true))
                return relativeCategories;
            var medicins = await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthId != disease.BeatyandHealthyId).Take(numberRelated - relativeCategories.Count()).ToListAsync();
            return relativeCategories.AppendData(medicins);
        }

    }
}
