using MYDoctor.Core.Domain.Entities;
using System.Threading.Tasks;
namespace MYDoctor.Core.Application.IRepository
{
    public interface ITableTrackUserRepository:IRepository<TableTrackUser>
    {
       Task<TableTrackUser> GetTrackUserAsync(string name);
       Task  RefreshUserNofificationAsync(string name);
    }
}
