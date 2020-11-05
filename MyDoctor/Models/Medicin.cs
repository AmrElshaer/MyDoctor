using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class Medicin
    {
        public Medicin()
        {
            this.DiseaseMedicins=new HashSet<DiseaseMedicin>();
        }
        public int Id { get; set; }
        
        public string Name { get; set; }
        [DataType(DataType.Currency)]
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
