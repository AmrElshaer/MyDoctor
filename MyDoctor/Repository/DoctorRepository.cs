
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Data;
using MyDoctor.Helper;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
using PagedList.Core;

namespace MyDoctor.Repository
{
    public class DoctorRepository:BaseRepository<Doctor>,IDoctorRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _ihostEnv;

        public DoctorRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment ihostEnv) :base(context)
        {
            _userManager = userManager;
            _ihostEnv = ihostEnv;
        }
      
        /// <summary>
        /// Add Doctor To Identity Table(User) and Add in Doctor Table
        /// </summary>
        /// <param name="doctor"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        private async Task RegisterDoctor(Doctor doctor,IFormFile image)
        {
            var imagePath= RegisterHelper.ConfigImagePath(image, _ihostEnv);
            var user = new ApplicationUser { UserName = doctor.Email, Email = doctor.Email,ImagePath = imagePath };
            var result = await _userManager.CreateAsync(user, doctor.Password);

            if (result.Succeeded)
            {

                doctor.ImagePath = imagePath;
                await InsertAsync(doctor);
                await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(doctor.Email), "Doctor");
                
            }

            
        }

        private async Task EditDoctor(Doctor doctor,IFormFile image)
        {
            if (image != null)
            {
                RegisterHelper.DeleteImage(doctor.ImagePath, _ihostEnv);
                doctor.ImagePath = RegisterHelper.ConfigImagePath(image, _ihostEnv);
            }

            
            var olddoctor =await _table.FindAsync(doctor.Id);
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

        public SearchResult<Doctor> GetSearchResult(string query, int pageNumber, int pageSize,int? category)
        {
            var searchHits = Search(query, category);
            var subset = searchHits.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var count = searchHits.Count();
            var searchResult = new SearchResult<Doctor>()
            {
                ItemsList = new StaticPagedList<Doctor>(subset, pageNumber, pageSize, count),
                SearchQuery = query
               ,IdRelated = category
            };
            return searchResult;
        }
        private IOrderedQueryable<Doctor> Search(string query,int? categoryId)
        {
            var doctors = _context.Doctor.Include(doc => doc.Category).Where(x =>
                (query == null || x.Name.ToLower().Contains(query.ToLower()))
                && (categoryId == null || x.CategoryId == categoryId)

            ).OrderByDescending(category => category.Id);
            return doctors;

        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor =await GetByIdAsync(id);
            var user =await  _userManager.FindByEmailAsync(doctor.Email);
            await  _userManager.DeleteAsync(user);
            await DeleteAsync(id);
        }

        public async Task CreateEdit(Doctor doctor,IFormFile image)
        {
            if (doctor.Id >0)
                await EditDoctor(doctor,image);
            else
              await RegisterDoctor(doctor, image);
           
        }
    }
}
