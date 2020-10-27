
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyDoctor.Infrastructure;
using MyDoctor.Models;

namespace MyDoctor.IRepository
{
    public interface IDoctorRepository:IRepository<Doctor>
    {
        Task<Doctor> GetDocWithPostsAsync(object id);
        Task<IdentityResult> RegisterDoctor(Doctor doctor);
    }
}
