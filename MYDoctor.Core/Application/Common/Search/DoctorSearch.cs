using System.Collections.Generic;
namespace MYDoctor.Core.Application.Common.Search
{
    public class DoctorSearch
    {
        public DoctorSearch()
        {
            Categories = new List<int>();
            Countries = new List<string>();
            Cities = new List<string>();
        }
        public string Name { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Cities { get; set; }
        public List<int> Categories { get; set; }
    }
}
