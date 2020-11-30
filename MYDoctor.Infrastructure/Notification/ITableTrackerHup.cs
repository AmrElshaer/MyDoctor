using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Notification
{
    public  interface ITableTrackerHup
    {
        Task TableTrackerNotifiy(IEnumerable<TableTrack> tableTracks,string name);
    }
}
