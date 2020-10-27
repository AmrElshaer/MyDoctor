using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Repository
{
    public class DoctorRepository:BaseRepository<Doctor>,IDoctorRepository
    {
        private readonly UserManager<CutomPropertiy> _userManager;
        public DoctorRepository(ApplicationDbContext context, UserManager<CutomPropertiy> userManager) :base(context)
        {
            _userManager = userManager;
        }


        public async Task<Doctor> GetDocWithPostsAsync(object id)
        {
          var resuilt= await  _table.Include(doctor => doctor.Posts).FirstOrDefaultAsync(doctor => doctor.Id == (int) id);
          return resuilt;
        }

        public async Task<IdentityResult> RegisterDoctor(Doctor doctor)
        {
            var user = new CutomPropertiy { UserName = doctor.Email, Email = doctor.Email };
            var result = await _userManager.CreateAsync(user, doctor.Password);

            if (result.Succeeded)
            {
                await InsertAsync(doctor);
                await SaveAsync();
                await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(doctor.Email), "Doctor");
            }

            return result;
        }
    }
}
