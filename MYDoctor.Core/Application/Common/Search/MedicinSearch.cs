using System.Collections.Generic;
namespace MYDoctor.Core.Application.Common.Search
{
    public class MedicinSearch
    {
        public MedicinSearch()
        {
            Categories = new List<int>();
          
        }
        public string Name { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal? Price { get; set; }
        public List<int> Categories { get; set; }

    }
}
