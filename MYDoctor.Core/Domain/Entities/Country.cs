using MYDoctor.Core.Domain.Common;
using System.Collections.Generic;
namespace MYDoctor.Core.Domain.Entities
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
