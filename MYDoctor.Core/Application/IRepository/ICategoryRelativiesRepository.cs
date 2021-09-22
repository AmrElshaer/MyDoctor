using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;

namespace MYDoctor.Core.Application.IRepository
{
    public interface ICategoryRelativiesRepository:IRepository<RelativeofBeatyandhealthy>
    {
        SearchResult<RelativeofBeatyandhealthy> GetSearchResult(SearchParamter searchParamter);

        Task CreateEdit(RelativeofBeatyandhealthy category);
       
        Task<RelativeBeatyandhealthyViewModel> GetRelativeCategoryAsync(int id, int numberRelated);
        Task<IEnumerable<RelativeofBeatyandhealthy>> SearchHits(SearchParamter searchParamter);
    }
}
