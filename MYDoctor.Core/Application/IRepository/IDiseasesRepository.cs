using System;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Domain.Entities;

namespace MYDoctor.Core.Application.IRepository
{
    public interface IDiseasesRepository:IRepository<Disease>
    {
        Task CreateEdit(Disease disease);
        SearchResult<Disease> GetSearchResult(SearchParamter searchParamter);
        Task<BaseViewModel> GetDiseaseAsync(int id, int numberRelated);
    }
}
