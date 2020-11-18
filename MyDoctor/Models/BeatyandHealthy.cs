using MyDoctor.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace MyDoctor.Models
{
    public class BeatyandHealthy:BaseEntity
    {
        public BeatyandHealthy()
        {

            RelativeofBeatyandhealthies =new HashSet<RelativeofBeatyandhealthy>();
            Doctors = new HashSet<Doctor>();
            Diseases = new HashSet<Disease>();
            Medicins= new HashSet<Medicin>();

        }
        
        public string Category { get; set; }
        public string  Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<RelativeofBeatyandhealthy> RelativeofBeatyandhealthies { get; set; }
        public ICollection<Medicin> Medicins { get; set; }
        public ICollection<Disease> Diseases { get; set; }
        [JsonIgnore]
        public  ICollection<Doctor>Doctors { get; set; }
    }
}
