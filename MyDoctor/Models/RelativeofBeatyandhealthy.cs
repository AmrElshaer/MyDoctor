using MyDoctor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class RelativeofBeatyandhealthy:BaseEntity
    {
       

        [DataType(dataType: DataType.Url)]
        public string ImageOrVideo { get; set; }
        public string Subject { get; set; }
        public string Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModiteDate { get; set; }
        public int BeatyandHealthId { get; set; }
        public BeatyandHealthy BeatyandHealthy { get; set; }
       
    }
}
