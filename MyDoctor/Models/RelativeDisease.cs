using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class RelativeDisease
    { 
        public string Id { get; set; }
        public string DiseaseName { get; set; }
        [DataType(dataType:DataType.Url)]
        public string ImageOrvideo { get; set; }
        public string subject { get; set; }
        public string DiseaseAddress { get; set; }
    }
}
