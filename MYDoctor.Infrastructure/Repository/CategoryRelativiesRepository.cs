using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
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
    }
}
