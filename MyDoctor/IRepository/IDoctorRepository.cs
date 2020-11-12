
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Infrastructure;
using MyDoctor.Models;

namespace MyDoctor.IRepository
{
    public interface IDoctorRepository:IRepository<Doctor>
    {

        Task DeleteDoctorAsync(int id);
        Task CreateEdit(Doctor doctor, IFormFile image);
        SearchResult<Doctor> GetSearchResult(string query, int pageNumber, int pageSize, int? category);
        IOrderedQueryable<Doctor> Search(string query=null, int? categoryId=null);
    }
}
