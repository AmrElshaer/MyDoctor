
using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
namespace MYDoctor.Core.Domain.Entities
{
    public class BeatyandHealthy:BaseEntity
    {
        public BeatyandHealthy()
        {

            RelativeofBeatyandhealthies =new HashSet<RelativeofBeatyandhealthy>();
            Doctors = new HashSet<Doctor>();
            Diseases = new HashSet<Disease>();
            Medicins= new HashSet<Medicin>();
            Posts = new HashSet<Post>();

        }
        
        public string Category { get; set; }
        public string  Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<RelativeofBeatyandhealthy> RelativeofBeatyandhealthies { get; set; }
        public ICollection<Medicin> Medicins { get; set; }
        public ICollection<Disease> Diseases { get; set; }
        public  ICollection<Doctor>Doctors { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
