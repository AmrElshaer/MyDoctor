using MyDoctor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class Country:BaseEntity
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }
       
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
