using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Core.Application.Common;
using MYDoctor.Infrastructure.Identity;
using MYDoctor.Helper.Infrastructure;
using System.Collections.Generic;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Core.Application.IHelper;
using System.Linq.Expressions;

namespace MYDoctor.Infrastructure.Repository
{
    public class MedicinRepository:BaseRepository<Medicin>,IMedicinRepository
    {
        private readonly ITableTrackNotification _tableTrackNotification;
        private readonly IDoctorHelper _doctorHelper;
        private readonly IDiseaseHelper _diseaseHelper;
        private readonly IPostHelper _postHelper;
        private readonly IMedicinHelper _medicinHelper;
        private readonly IRelativeCategoryHelper _relativeCategoryHelper;
        private readonly ICategoryHelper _categoryHelper;

        public MedicinRepository(ApplicationDbContext context, IMedicinHelper medicinHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper, IDiseaseHelper diseaseHelper, IDoctorHelper doctorHelper, ITableTrackNotification tableTrackNotification, ICategoryHelper categoryHelper) : base(context)
        {
            _tableTrackNotification = tableTrackNotification;
            _doctorHelper = doctorHelper;
            _diseaseHelper = diseaseHelper;
            _postHelper = postHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
            _medicinHelper = medicinHelper;
            _categoryHelper = categoryHelper;
        }

        public SearchResult<Medicin> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(x =>
               (string.IsNullOrEmpty(searchParamter.SearchQuery) || x.Name.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
               && (searchParamter.CreateFrom == null || x.CreateDate >= searchParamter.CreateFrom)
               && (searchParamter.CreateTo == null || x.CreateDate <= searchParamter.CreateTo)
                , m => m.OrderByDescending(a => a.Id),
                m => m.BeatyandHealthy,
                m => m.DiseaseMedicins
                ).Select(m => new Medicin()
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

            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
            return searchResult;
        }

        public async Task CreateEdit(Medicin medicin)
        {
            if (medicin.Id >0)
            {
               
                medicin.ModifiedDate = DateTime.Now.Date;
                var medicine =await _table.Include(m=>m.DiseaseMedicins).FirstOrDefaultAsync(m=>m.Id==medicin.Id);
                //Delete If NOt Longer Exist
                medicine.DiseaseMedicins.ToList().ForEach(dm=>{
                    if (medicin.DiseaseMedicins.All(a => a.DiseaseId != dm.DiseaseId))
                             medicine.DiseaseMedicins.Remove(dm);
                });
                //Add If Not Exist
                var medicineComparer=new MedicineComparer();
                  medicin.DiseaseMedicins.ToList().ForEach(dm=>
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
        public async Task<IEnumerable<Medicin>> GEtMedicinsAsync(MedicinSearch medicinSearch) {
            var medicins = await GetAll(
              d => (!medicinSearch.Categories.Any() || medicinSearch.Categories.Contains(d.BeatyandHealthyId))
              && (string.IsNullOrEmpty(medicinSearch.Name) || d.Name.ToLower().Contains(medicinSearch.Name.ToLower()))
              && (!medicinSearch.Price.HasValue || d.Price <= medicinSearch.Price.Value)
               , d => d.OrderByDescending(o => o.Id), d => d.BeatyandHealthy).ToListAsync();
            var pricerange = PriceRange();
            medicinSearch.MinPrice = pricerange.minprice;
            medicinSearch.MaxPrice = pricerange.maxprice;
            return medicins;

        }
        public (decimal minprice, decimal maxprice) PriceRange()
        {
            var maxprice= _context.Medicin.Max(m=>m.Price);
            var minprice= _context.Medicin.Min(m=>m.Price);
            return (minprice, maxprice);
        }

        public async Task<MedicinViewModel> GetMedicinAsync(int id,int numberRelated)
        {
            var medicin =await  _context.Medicin.Include(m=>m.BeatyandHealthy).
                ThenInclude(c=>c.Doctors).Include(m=>m.BeatyandHealthy.Posts).
                Include(m=>m.BeatyandHealthy.RelativeofBeatyandhealthies)
                .Include(m=>m.DiseaseMedicins).ThenInclude(dm=>dm.Disease).FirstOrDefaultAsync(m=>m.Id==id);

            var categoryId = medicin.BeatyandHealthyId;
            var result = new MedicinViewModel(medicin, numberRelated)
                .WithMedicin(this._medicinHelper, categoryId)
                .WithRelativeCategory(this._relativeCategoryHelper, categoryId)
                .WithDisease(this._diseaseHelper, categoryId)
                .WithDoctors(this._doctorHelper, categoryId)
                .WithPosts(this._postHelper, categoryId)
                .WithCategories(this._categoryHelper, categoryId).Build();
            return result;
        }
       
    }
}
