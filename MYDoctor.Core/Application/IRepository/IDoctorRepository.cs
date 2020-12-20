using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MYDoctor.Core.Application.IRepository
{
    public interface IDoctorRepository:IRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetDoctorsAsync(DoctorSearch doctorSearch);
        Task DeleteDoctorAsync(int id);
        Task CreateEdit(Doctor doctor);
        SearchResult<Doctor> GetSearchResult(SearchParamter searchParamter);
        Task<DoctorViewModel> DoctorProfileAsync(string doctorEmail);


    }
}
