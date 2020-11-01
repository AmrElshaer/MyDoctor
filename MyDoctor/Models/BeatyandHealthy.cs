using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class BeatyandHealthy
    {
       
        public int Id { get; set; }
        public string Catagory { get; set; }
        public string  Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
