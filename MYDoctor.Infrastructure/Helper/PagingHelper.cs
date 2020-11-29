using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MYDoctor.Infrastructure.Helper
{
    public class PagingHelper
    {
        public static SearchResult<T> PagingModel<T>(IQueryable<T> searchHits,SearchParamter searchParamter) where T : BaseEntity
        {
            var subset = searchHits.Skip((searchParamter.Page.Value - 1) * searchParamter.PageSize).Take(searchParamter.PageSize);
            searchParamter.TotalCount = searchHits.Count();
            var searchResult = new SearchResult<T>()
            {
                ItemsList = subset,
                SearchParamter = searchParamter
            };
            return searchResult;
        }
    }
}
