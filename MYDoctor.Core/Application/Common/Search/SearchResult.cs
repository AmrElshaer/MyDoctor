using System.Collections.Generic;
namespace MYDoctor.Core.Application.Common.Search
{
    public class SearchResult<T> where T :class
    {
        public IEnumerable<T> ItemsList { get; set; }
        public SearchParamter    SearchParamter { get; set; }


    }
}
