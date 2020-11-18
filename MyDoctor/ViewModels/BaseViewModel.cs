
using System.Collections.Generic;
using MyDoctor.Models;

namespace MyDoctor.ViewModels
{
    public abstract class BaseViewModel
    {
         protected BaseViewModel()
         {
            Categories = new List<BeatyandHealthy>();
            Doctors = new List<Doctor>();
            Diseases = new List<Disease>();
            RelativeCategories = new List<RelativeofBeatyandhealthy>();
            Medicins = new List<Medicin>();

         }
         public IEnumerable<BeatyandHealthy> Categories { get; set; }
         public IEnumerable<Doctor> Doctors { get; set; }
         public IEnumerable<Medicin> Medicins { get; set; }
         public IEnumerable<Disease> Diseases { get; set; }
         public IEnumerable<RelativeofBeatyandhealthy> RelativeCategories { get; set; }
    }
}
