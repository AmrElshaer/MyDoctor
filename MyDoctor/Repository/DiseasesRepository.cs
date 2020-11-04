
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
    public class DiseasesRepository:BaseRepository<Disease>,IDiseasesRepository
    {
        public DiseasesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateEdit(Disease disease)
        {
            if (disease.Id > 0)
            {
                disease.ModifiedDate = DateTime.Now;
                await Update(disease);
            }
            else
            {
                disease.CreateDate = DateTime.Now;
                await InsertAsync(disease);
            }
            
        }

        public SearchResult<Disease> GetSearchResult(string query, int pageNumber, int pageSize, DateTime? createFrom, DateTime? createTo)
        {
            var searchHits = Search(query, createFrom, createTo);
            var subset = searchHits.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var count = searchHits.Count();
            var searchResult = new SearchResult<Disease>()
            {
                ItemsList = new StaticPagedList<Disease>(subset, pageNumber, pageSize, count),
                SearchQuery = query
                ,
                CreateFrom = createFrom,
                CreateTo = createTo
            };
            return searchResult;
        }
        public IOrderedQueryable<Disease> Search(string query, DateTime? createFrom, DateTime? createTo)
        {
            var categories = _context.Disease.Include(d=>d.BeatyandHealthy).Where(x =>
                (query == null || x.DiseaseName.ToLower().Contains(query.ToLower()))
                && (createFrom == null || x.CreateDate >= createFrom)
                && (createTo == null || x.CreateDate <= createTo)
            ).OrderByDescending(category => category.Id);
            return categories;

        }
    }
}
