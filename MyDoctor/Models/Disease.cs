using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class Disease
    {
        
      public  int Id { get; set; }
        [Required]
        public string DiseaseName { get; set; }
        public string Subject { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        public string Protection { get; set; }
        public string Medicin { get; set; }
        public string Reasons { get; set; }

    }
}
