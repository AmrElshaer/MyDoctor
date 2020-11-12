using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Infrastructure;
using MyDoctor.Models;

namespace MyDoctor.IRepository
{
    public interface IMedicinRepository:IRepository<Medicin>
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
        SearchResult<Medicin> GetSearchResult(string query, int page, int pageSize, DateTime? createFrom, DateTime? createTo);
        Task CreateEdit(Medicin medicin);
        /// <summary>
        /// Search Based On Filter Parameter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="createFrom"></param>
        /// <param name="createTo"></param>
        /// <returns></returns>
        IOrderedQueryable<Medicin> Search(string query= null, DateTime? createFrom = null, DateTime? createTo = null);
    }
}
