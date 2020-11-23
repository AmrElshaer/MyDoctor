using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Application.Common.Search
{
    public class SearchParamter
    {
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public string SearchQuery { get; set; }
        public DateTime? CreateFrom { get; set; }
        public DateTime? CreateTo { get; set; }
        public int? IdRelated { get; set; }
        public int TotalCount { get; set; }
    }
}
