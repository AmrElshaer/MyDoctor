using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
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
    }
}
