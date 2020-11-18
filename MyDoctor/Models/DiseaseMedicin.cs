using MyDoctor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class DiseaseMedicin:BaseEntity
    {
        
        public int DiseaseId { get; set; }
        public int MedicinId { get; set; }
        public Disease Disease { get; set; }
        public Medicin Medicin { get; set; }
    }
}
