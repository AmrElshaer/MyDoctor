using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Models;

namespace MyDoctor.ViewModels
{
    public interface IBaseViewModel
    {
         IEnumerable<BeatyandHealthy> Categories { get; set; }
         IEnumerable<Doctor> Doctors { get; set; }
         IEnumerable<Medicin> Medicins { get; set; }
         IEnumerable<Disease> Diseases { get; set; }
         IEnumerable<RelativeofBeatyandhealthy> RelativeCategories { get; set; }
    }
}
