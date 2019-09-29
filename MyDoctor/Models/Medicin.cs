using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class Medicin
    {
        public int id { get; set; }
        
        public string name { get; set; }
       [DataType(DataType.Currency)]
        public decimal price { get; set; }
        public  string affects { get; set; }
        public string indications { get; set; }
        public string diseasespecific { get; set; }
    }
}
