using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;


namespace MYDoctor.Core.Domain.Entities
{
    public class Medicin:BaseEntity
    {
        public Medicin()
        {
            this.DiseaseMedicins=new HashSet<DiseaseMedicin>();
        }
       
        public string Name { get; set; }
        public decimal Price { get; set; }
        public  string Affects { get; set; }
        public string Indications { get; set; }
        public int BeatyandHealthyId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public BeatyandHealthy BeatyandHealthy { get; set; }
        public string Image { get; set; }
        public ICollection<DiseaseMedicin>  DiseaseMedicins { get; set; }
    }
}
