using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Models;

namespace MyDoctor.ViewModels
{
    public class DashBoardViewModel
    {
        public IEnumerable<BeatyandHealthy> Categories { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Medicin> Medicins { get; set; }
        public IEnumerable<Disease> Diseases { get; set; }

    }
}
