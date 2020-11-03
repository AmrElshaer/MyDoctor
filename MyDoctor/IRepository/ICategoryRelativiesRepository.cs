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
    public interface ICategoryRelativiesRepository:IRepository<RelativeofBeatyandhealthy>
    {
        SearchResult<RelativeofBeatyandhealthy> GetSearchResult(string query, int pageNumber, int pageSize,
            DateTime? createFrom, DateTime? createTo, int? beatyandHealthId);

        IOrderedQueryable<RelativeofBeatyandhealthy> Search(string query, DateTime? createFrom,
            DateTime? createTo,int? beatyandHealthId);
        
        Task CreateEdit(RelativeofBeatyandhealthy category);
    }
}
