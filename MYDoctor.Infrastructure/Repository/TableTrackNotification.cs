using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class TableTrackNotification:BaseRepository<TableTrack>,ITableTrackNotification
    {
        public TableTrackNotification(ApplicationDbContext context) : base(context)
        {

        }
        public async Task InsertAsync(string title,string content,string controller,string action,string image,int itemId) {
            content = content?.Length < 100 ? content : content.Substring(0, 100);
            await InsertAsync(new TableTrack()
            {
                Title = title,
                Content = content,
                Controller = controller,
                Action = action,
                CreateDate = DateTime.Now,
                Image = image,
                ItemId = itemId
            });
        }
    }
}
