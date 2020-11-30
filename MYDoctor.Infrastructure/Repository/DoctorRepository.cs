using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.File;
using MYDoctor.Infrastructure.Helper;
using MYDoctor.Infrastructure.Identity;
namespace MYDoctor.Infrastructure.Repository
{
    public class DoctorRepository:BaseRepository<Doctor>,IDoctorRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileConfig _fileConfig;
        private readonly ITableTrackNotification _tableTrackNotification;

        public DoctorRepository(ApplicationDbContext context,ITableTrackNotification tableTrackNotification ,UserManager<ApplicationUser> userManager,IFileConfig fileConfig) :base(context)
        {
            _userManager = userManager;
            _fileConfig = fileConfig;
            _tableTrackNotification = tableTrackNotification;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAsync(DoctorSearch doctorSearch) {
            var doctors = await GetAll(
               d => (!doctorSearch.Categories.Any() || doctorSearch.Categories.Contains(d.CategoryId))
               && (!doctorSearch.Countries.Any() || doctorSearch.Countries.Contains(d.Country))
               && (!doctorSearch.Cities.Any() || doctorSearch.Cities.Contains(d.City))
               && (string.IsNullOrEmpty(doctorSearch.Name) || d.Name.ToLower().Contains(doctorSearch.Name.ToLower()))
                , d => d.OrderByDescending(o => o.Id), d => d.Category).ToListAsync();
            return doctors;

        }
        private async Task RegisterDoctor(Doctor doctor)
        {
            
            var user = new ApplicationUser { UserName = doctor.Email, Email = doctor.Email,ImagePath = doctor.ImagePath };
            var result = await _userManager.CreateAsync(user, doctor.Password);

            if (result.Succeeded)
            {
                await InsertAsync(doctor);
                await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(doctor.Email), "Doctor");
                await _tableTrackNotification.InsertAsync(doctor.Name, doctor.Others, "Doctor", "Profile", $"/images/${doctor.ImagePath}", doctor.Id);

            }


        }

        private async Task EditDoctor(Doctor doctor)
        {
            
            var olddoctor =await _table.FindAsync(doctor.Id);
            //Delete Old Images
            if (!string.IsNullOrEmpty(doctor.ImagePath))
                  _fileConfig.DeleteFile(olddoctor.ImagePath,"images"); 
            //Update Doctor in Identity Table
            var user= await _userManager.FindByEmailAsync(olddoctor.Email);
            user.Email = doctor.Email;
            user.UserName = doctor.Name;
            user.ImagePath = doctor.ImagePath;
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, doctor.Password);
            await _userManager.UpdateAsync(user);
            //Update Doctor in Table
            _context.Entry(olddoctor).CurrentValues.SetValues(doctor);
            await _context.SaveChangesAsync();
            
        }

        public SearchResult<Doctor> GetSearchResult(SearchParamter searchParamter)
        {
            var searchHits = GetAll(
                x =>
                (string.IsNullOrEmpty(searchParamter.SearchQuery) || x.Name.ToLower().Contains(searchParamter.SearchQuery.ToLower()))
                && (searchParamter.IdRelated == null || x.CategoryId == searchParamter.IdRelated),
                d => d.OrderByDescending(a => a.Id),
                   d=>d.Category
                );
            var searchResult = PagingHelper.PagingModel(searchHits, searchParamter);
            return searchResult;
        }
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor =await GetByIdAsync(id);
            var user =await  _userManager.FindByEmailAsync(doctor.Email);
            await  _userManager.DeleteAsync(user);
            await DeleteAsync(id);
            if (!string.IsNullOrEmpty(doctor.ImagePath))
                _fileConfig.DeleteFile(doctor.ImagePath, "images");
            await _context.SaveChangesAsync();
        }

        public async Task CreateEdit(Doctor doctor)
        {
            if (doctor.Id >0)
                await EditDoctor(doctor);
            else
              await RegisterDoctor(doctor);
           
        }
    }
}
