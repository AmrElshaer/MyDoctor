using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
using PagedList.Core;

namespace MyDoctor.Repository
{
    public class CategoryRepository:BaseRepository<BeatyandHealthy>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

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
        public async Task CreateEdit(BeatyandHealthy category)
        {
            if (category.Id != 0)
            {
                
                    category.ModifiedDate=DateTime.Now.Date;
                    await Update(category);
               
            }
            else
            {
                category.CreateDate=DateTime.Now.Date;
                await InsertAsync(category);
               
               
            }

            

        }
    }
}
