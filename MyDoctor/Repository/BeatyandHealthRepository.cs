using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
using PagedList.Core;

namespace MyDoctor.Repository
{
    public class BeatyandHealthRepository:BaseRepository<BeatyandHealthy>,IBeatyandHealthRepository
    {
        public BeatyandHealthRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SearchResult GetSearchResult(string query, int page, int pageSize,DateTime? createFrom,DateTime? createTo)
        {
            var searchHits = _context.BeatyandHealthy.Where(x =>
                (query==null||x.Catagory.ToLower().Contains(query.ToLower()))
            &&(createFrom==null||x.CreateDate>=createFrom)
                &&(createTo==null||x.CreateDate<=createTo)
            ).OrderByDescending(category=>category.Id);
            var subset = searchHits.Skip((page - 1) * pageSize).Take(pageSize);
            var count = searchHits.Count();
            var searchResult = new SearchResult()
            {
                Category =new StaticPagedList<BeatyandHealthy>(subset, page, pageSize, count),
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
