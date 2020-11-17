using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.ViewModels
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
