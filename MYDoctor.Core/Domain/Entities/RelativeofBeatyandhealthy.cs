using MYDoctor.Core.Domain.Common;
using System;

namespace MYDoctor.Core.Domain.Entities
{
    public class RelativeofBeatyandhealthy:BaseEntity
    {
       

        
        public string ImageOrVideo { get; set; }
        public string Subject { get; set; }
        public string Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModiteDate { get; set; }
        public int BeatyandHealthId { get; set; }
        public BeatyandHealthy BeatyandHealthy { get; set; }
       
    }
}
