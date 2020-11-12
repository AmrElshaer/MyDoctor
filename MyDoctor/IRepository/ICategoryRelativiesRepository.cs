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

        IOrderedQueryable<RelativeofBeatyandhealthy> Search(string query=null, DateTime? createFrom=null,
            DateTime? createTo=null,int? beatyandHealthId=null);
        
        Task CreateEdit(RelativeofBeatyandhealthy category);
    }
}
