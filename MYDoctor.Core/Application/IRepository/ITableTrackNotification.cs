using MYDoctor.Core.Domain.Entities;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IRepository
{
    public interface ITableTrackNotification:IRepository<TableTrack>
    {
        Task InsertAsync(string title, string content, string controller, string action, string image, int itemId);
    }
}
