
using System.Threading.Tasks;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Infrastructure;
using MyDoctor.Models;
using System;
namespace MyDoctor.IRepository
{
    public interface IBeatyandHealthRepository:IRepository<BeatyandHealthy>
    {
        SearchResult GetSearchResult(string query, int page, int pageSize, DateTime? createFrom, DateTime? createTo);
        Task CreateEdit(BeatyandHealthy category);
    }
}
