using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Specific { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }    
        
    }
}
