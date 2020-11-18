
using System.Threading.Tasks;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Infrastructure;
using MyDoctor.Models;
using System;
using System.Linq;
using MyDoctor.ViewModels;

namespace MyDoctor.IRepository
{
    public interface ICategoryRepository:IRepository<BeatyandHealthy>
    {
        Task<BaseViewModel> GetCategoryAsync(int categoryId, int numberRelated);
        SearchResult<BeatyandHealthy> GetSearchResult(string query, int page, int pageSize, DateTime? createFrom, DateTime? createTo);
        Task CreateEdit(BeatyandHealthy category);
        Task<BaseViewModel> GetBoardViewModel(int pageSize);


    }
}
