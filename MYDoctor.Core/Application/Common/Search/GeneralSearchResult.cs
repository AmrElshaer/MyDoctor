using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Application.Common.Search
{
    public class GeneralSearchResult
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
    }
}
