
using System.Threading.Tasks;
using System.Collections.Generic;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;

namespace MYDoctor.Core.Application.IRepository
{
    public interface ICategoryRepository:IRepository<BeatyandHealthy>
    {
        Task<BaseViewModel> GetCategoryAsync(int categoryId, int numberRelated);
        SearchResult<BeatyandHealthy> GetSearchResult(SearchParamter searchParamter);
        Task CreateEdit(BeatyandHealthy category);
        Task<BaseViewModel> GetBoardViewModel(int pageSize);
        Task<IEnumerable<GeneralSearchResult>> GeneralSearchAsync(string searchval);
        Task<IEnumerable<BeatyandHealthy>> GetAdminBoard();
    }
}
