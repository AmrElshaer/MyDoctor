using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
namespace MYDoctor.Core.Application.Common
{
    public abstract  class BaseViewModel<T> where T:class
    {
        
        protected BaseViewModel()
         {
           
            Categories = new List<BeatyandHealthy>();
            Doctors = new List<Doctor>();
            Diseases = new List<Disease>();
            RelativeCategories = new List<RelativeofBeatyandhealthy>();
            Medicins = new List<Medicin>();
            Posts = new List<Post>();

         }
        public T Model { get; set; }

        public IEnumerable<BeatyandHealthy> Categories { get; set; }
         public IEnumerable<Doctor> Doctors { get; set; }
         public IEnumerable<Medicin> Medicins { get; set; }
         public IEnumerable<Disease> Diseases { get; set; }
         public IEnumerable<RelativeofBeatyandhealthy> RelativeCategories { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
