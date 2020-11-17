using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
using MyDoctor.ViewModels;
using PagedList.Core;

namespace MyDoctor.Repository
{
    public class CategoryRelativiesRepository:BaseRepository<RelativeofBeatyandhealthy>,ICategoryRelativiesRepository
    {
        public CategoryRelativiesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SearchResult<RelativeofBeatyandhealthy> GetSearchResult(string query, int page, int pageSize,
            DateTime? createFrom, DateTime? createTo, int? beatyandHealthId)
        {
            var searchHits = GetAll(
                x =>
                (query == null || x.Address.ToLower().Contains(query.ToLower()))
                && (createFrom == null || x.CreateDate >= createFrom)
                && (createTo == null || x.CreateDate <= createTo)
                && (beatyandHealthId == null || x.BeatyandHealthy.Id == beatyandHealthId.Value),
                rc => rc.OrderByDescending(a => a.Id),
                rc=>rc.BeatyandHealthy
                
                );
            var subset = searchHits.Skip((page - 1) * pageSize).Take(pageSize);
            var count = searchHits.Count();
            var searchResult = new SearchResult<RelativeofBeatyandhealthy>()
            {
                ItemsList = new StaticPagedList<RelativeofBeatyandhealthy>(subset, page, pageSize, count),
                SearchQuery = query
                ,
                CreateFrom = createFrom,
                CreateTo = createTo,
                IdRelated = beatyandHealthId
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
    }
}
