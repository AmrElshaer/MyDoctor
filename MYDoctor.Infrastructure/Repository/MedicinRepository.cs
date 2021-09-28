using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Helper.Infrastructure;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class MedicinRepository : BaseRepository<Medicin>, IMedicinRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;
        private readonly IServiceBuilder serviceBuilder;

        public MedicinRepository(ApplicationDbContext context, IServiceBuilder serviceBuilder, ITableTrackNotification tableTrackNotification) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
            this.serviceBuilder = serviceBuilder;
        }

        public SearchResult<Medicin> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(ApplyFiliter(searchParamter))
                .Include(m => m.BeatyandHealthy).Include(m => m.DiseaseMedicins).OrderByDescending(a => a.Id)
                .Select(m => new Medicin()
                {
                    Name = m.Name,
                    Affects = m.Affects,
                    Indications = m.Indications,
                    Id = m.Id,
                    Image = m.Image,
                    BeatyandHealthy = m.BeatyandHealthy,
                    Price = m.Price,
                    DiseaseMedicins = m.DiseaseMedicins,
                    CreateDate = m.CreateDate
                });

            return PagingHelper.PagingModel(searchHits, searchParamter);
        }

        private static Expression<Func<Medicin, bool>> ApplyFiliter(SearchParamter searchParamter)
        {
            return x =>
                           (string.IsNullOrEmpty(searchParamter.SearchQuery) || x.Name.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                           && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
                           && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo);
        }

        public async Task CreateEdit(Medicin medicin)
        {
            if (medicin.Id > 0)
            {
                medicin.ModifiedDate = DateTime.Now.Date;
                var medicine = await _table.Include(m => m.DiseaseMedicins).FirstOrDefaultAsync(m => m.Id == medicin.Id);
                //Delete If NOt Longer Exist
                medicine.DiseaseMedicins.ToList().ForEach(dm =>
                {
                    if (medicin.DiseaseMedicins.All(a => a.DiseaseId != dm.DiseaseId))
                        medicine.DiseaseMedicins.Remove(dm);
                });
                //Add If Not Exist
                var medicineComparer = new MedicineComparer();
                medicin.DiseaseMedicins.ToList().ForEach(dm =>
                {
                    if (!medicine.DiseaseMedicins.Contains(dm, medicineComparer))
                        medicine.DiseaseMedicins.Add(dm);
                });
                //Update Only Simple Properties
                _context.Entry(medicine).CurrentValues.SetValues(medicin);
                await _context.SaveChangesAsync();
            }
            else
            {
                medicin.CreateDate = DateTime.Now.Date;
                await InsertAsync(medicin);
                //Add To Notification Table
                await _tableTrackNotification.InsertAsync(medicin.Name, medicin.Indications, "Medicins", "Details", medicin.Image, medicin.Id);
            }
        }

        public async Task<MedicinViewModel> GEtMedicinsAsync(MedicinSearch medicinSearch)
        {
            var medicins = await GetAll(MedicinFiliter(medicinSearch)).Include(d => d.BeatyandHealthy).OrderByDescending(o => o.Id).ToListAsync();
            medicinSearch.MinPrice = GetMinPrice();
            medicinSearch.MaxPrice = GetMaxPrice();
            return new MedicinViewModel(medicins, medicinSearch);
        }

        private static Expression<Func<Medicin, bool>> MedicinFiliter(MedicinSearch medicinSearch)
        {
            return
                          d => (!medicinSearch.Categories.Any() || medicinSearch.Categories.Contains(d.BeatyandHealthyId))
                          && (string.IsNullOrEmpty(medicinSearch.Name) || d.Name.ToLower().Contains(medicinSearch.Name.ToLower()))
                          && (!medicinSearch.Price.HasValue || d.Price <= medicinSearch.Price.Value);
        }

        private decimal GetMinPrice()
        {
            return _context.Medicin.Min(m => m.Price);
        }

        private decimal GetMaxPrice()
        {
            return _context.Medicin.Max(m => m.Price);
        }

        public async Task<BaseViewModel<Medicin>> GetMedicinAsync(int id, int numberRelated)
        {
            return serviceBuilder.BuildViewModel(new MedicinViewModel(await GetMedicin(id), numberRelated));
        }

        private async Task<Medicin> GetMedicin(int id)
        {
            return await _context.Medicin.Include(m => m.BeatyandHealthy).
                ThenInclude(c => c.Doctors).Include(m => m.BeatyandHealthy.Posts).
                Include(m => m.BeatyandHealthy.RelativeofBeatyandhealthies)
                .Include(m => m.DiseaseMedicins).ThenInclude(dm => dm.Disease).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}