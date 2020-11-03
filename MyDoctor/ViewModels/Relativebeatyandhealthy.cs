using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.ViewModels
{
    public class Relativebeatyandhealthy
    {
        public int Id { get; set; }

        [DataType(dataType: DataType.Url)]
        public string ImageOrvideo { get; set; }
        public string Subject { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public  int BeatyandHealthy { get; set; }
    }
}
