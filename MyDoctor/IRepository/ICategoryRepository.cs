
using System.Threading.Tasks;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Infrastructure;
using MyDoctor.Models;
using System;
using System.Linq;

namespace MyDoctor.IRepository
{
    public interface ICategoryRepository:IRepository<BeatyandHealthy>
    {
        /// <summary>
        /// Search With Paging 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="createFrom"></param>
        /// <param name="createTo"></param>
        /// <returns></returns>
        SearchResult<BeatyandHealthy> GetSearchResult(string query, int page, int pageSize, DateTime? createFrom, DateTime? createTo);
        Task CreateEdit(BeatyandHealthy category);
        /// <summary>
        /// Search Based On Filter Parameter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="createFrom"></param>
        /// <param name="createTo"></param>
        /// <returns></returns>
        IOrderedQueryable<BeatyandHealthy> Search(string query, DateTime? createFrom, DateTime? createTo);
    }
}
