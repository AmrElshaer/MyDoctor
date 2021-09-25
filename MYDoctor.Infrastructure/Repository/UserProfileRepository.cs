using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace MYDoctor.Infrastructure.Repository
{
    public class UserProfileRepository:BaseRepository<UserProfile>,IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDbContext context):base(context)
        {
        }
        public async Task InsertAsync(string email,string imagePath) {
            await InsertAsync(new UserProfile { Name = email.Split('@')[0], Email = email, ImagePath = imagePath });
        }
        public async Task<UserProfileViewModel> GetUserProfileAsync(string userEmail) {
            
            var user = await GetFirstAsync(a=>a.Email==userEmail);
            var posts = _context.Posts.Include(a=>a.User).Include(a=>a.Likes).Include(a=>a.DisLikes).OrderByDescending(a=>a.Id);
            var result = new UserProfileViewModel()
            {
                Model = user,
                Posts=posts
            };
            return result;
        }
        
    }
}
