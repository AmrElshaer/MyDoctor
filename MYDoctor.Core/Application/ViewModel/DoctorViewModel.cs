using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
namespace MYDoctor.Core.Application.ViewModel
{
    public class DoctorViewModel:BaseViewModel
    {
        public DoctorViewModel()
        {

        }
        public DoctorViewModel(IEnumerable<Doctor> doctors,DoctorSearch doctorSearch)
        {
            this.Doctors = doctors;
            this.DoctorSearch = doctorSearch;
        }
        public Doctor Doctor { get; set; }
        public DoctorSearch DoctorSearch { get; set; }
    }
}
