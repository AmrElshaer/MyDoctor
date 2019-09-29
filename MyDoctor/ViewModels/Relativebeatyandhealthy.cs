using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.ViewModels
{
    public class Relativebeatyandhealthy
    {
        public string Id { get; set; }

        [DataType(dataType: DataType.Url)]
        public string ImageOrvideo { get; set; }
        public string subject { get; set; }
        public string Address { get; set; }
        public  int beatyandHealthy { get; set; }
    }
}
