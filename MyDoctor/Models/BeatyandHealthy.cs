using System;
using System.Collections.Generic;
namespace MyDoctor.Models
{
    public class BeatyandHealthy
    {
        public BeatyandHealthy()
        {

            RelativeofBeatyandhealthies =new HashSet<RelativeofBeatyandhealthy>();
        }
        public int Id { get; set; }
        public string Category { get; set; }
        public string  Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<RelativeofBeatyandhealthy> RelativeofBeatyandhealthies { get; set; }
    }
}
