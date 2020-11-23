using System;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common.Search;

using MYDoctor.Core.Domain.Entities;

namespace MYDoctor.Core.Application.IRepository
{
    public interface ICategoryRelativiesRepository:IRepository<RelativeofBeatyandhealthy>
    {
        SearchResult<RelativeofBeatyandhealthy> GetSearchResult(SearchParamter searchParamter);

        Task CreateEdit(RelativeofBeatyandhealthy category);
    }
}
