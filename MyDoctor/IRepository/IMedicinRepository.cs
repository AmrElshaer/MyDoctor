using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Infrastructure;
using MyDoctor.Models;
using MyDoctor.ViewModels;

namespace MyDoctor.IRepository
{
    public interface IMedicinRepository:IRepository<Medicin>
    {
      
        SearchResult<Medicin> GetSearchResult(string query, int page, int pageSize, DateTime? createFrom, DateTime? createTo);
        Task CreateEdit(Medicin medicin);
        Task<MedicinViewModel> GetMedicinAsync(int id, int numberRelated);
        (decimal minprice, decimal maxprice) PriceRange();
      }
}
