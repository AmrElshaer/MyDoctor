using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.ViewModels
{
    public class BeatyandHealthviewModel
    {
        public int Id { get; set; }
        public string Catagory { get; set; }
        public string Image { get; set; }
        public IEnumerable<RelativeofBeatyandhealthy> RelativeofBeatyandhealthies { get; set; }
       

        //[DataType(dataType: DataType.Url)]
        //public string ImageOrvideo { get; set; }
        //public string subject { get; set; }
        //public string Address { get; set; }
        //public virtual BeatyandHealthy beatyandHealthy { get; set; }
    }
}
