using System.Collections.Generic;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;

namespace MYDoctor.Core.Application.IRepository
{
    public interface IMedicinRepository:IRepository<Medicin>
    {
        Task<IEnumerable<Medicin>> GEtMedicinsAsync(MedicinSearch medicinSearch);
        SearchResult<Medicin> GetSearchResult(SearchParamter searchParamter);
        Task CreateEdit(Medicin medicin);
        Task<BaseViewModel<Medicin>> GetMedicinAsync(int id, int numberRelated);
        (decimal minprice, decimal maxprice) PriceRange();
      }
}
