using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Models;
using PagedList.Core;

namespace MyDoctor.Areas.Admin.Models
{
    public class SearchResult
    {
        public IPagedList<BeatyandHealthy> Category { get; set; }

        public string SearchQuery { get; set; }
        public DateTime? CreateFrom { get; set; }
        public DateTime? CreateTo { get; set; }
    }
}
