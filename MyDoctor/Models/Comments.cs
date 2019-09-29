using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class Comments
    {
        public int Id{ get; set; }
        public string Commment { get; set; }
        public string UserName { get; set; }
        public string  ImagePath { get; set; }
        public string DiseaseName { get; set; }
    }
}
