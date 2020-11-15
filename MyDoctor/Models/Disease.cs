using MyDoctor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class Disease:BaseEntity
    {
        
       
        [Required]
        public string DiseaseName { get; set; }
        public string Subject { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        public string Protection { get; set; }
       
        public string Reasons { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? BeatyandHealthyId { get; set; }
        public ICollection<DiseaseMedicin> DiseaseMedicins  { get; set; }
        public BeatyandHealthy BeatyandHealthy { get; set; }

    }
}
