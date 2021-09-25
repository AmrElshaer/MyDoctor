using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
namespace MYDoctor.Core.Application.ViewModel
{
    public class DoctorViewModel:BaseViewModel<Doctor>
    {
        
        public DoctorSearch DoctorSearch { get; set; }
        public DoctorViewModel(IEnumerable<Doctor> doctors,DoctorSearch doctorSearch)
        {
            this.Doctors = doctors;
            this.DoctorSearch = doctorSearch;
        }
        
        public DoctorViewModel(Doctor doctor, IEnumerable<Post> posts)
        {
            this.Model = doctor;
            this.Posts = posts;
        }

     
    }
}
