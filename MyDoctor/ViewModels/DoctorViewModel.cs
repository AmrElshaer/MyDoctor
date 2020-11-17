using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.ViewModels
{
    public class DoctorViewModel
    {
       
        public DoctorViewModel(IEnumerable<Doctor> doctors,DoctorSearch doctorSearch)
        {
            this.Doctors = doctors;
            this.DoctorSearch = doctorSearch;
        }
        public IEnumerable<Doctor> Doctors { get; set; }
        public DoctorSearch DoctorSearch { get; set; }
    }
}
