using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IRepository
{
    public interface IUserProfileRepository:IRepository<UserProfile>
    {
        Task InsertAsync(string email, string imagePath);
        Task<UserProfileViewModel> GetUserProfileAsync(string userEmail);
    }
}
