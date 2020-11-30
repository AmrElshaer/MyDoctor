using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class TableTrackUserRepository:BaseRepository<TableTrackUser>,ITableTrackUserRepository
    {
        public TableTrackUserRepository(ApplicationDbContext context):base(context)
        {

        }

        public async Task<TableTrackUser> GetTrackUserAsync(string name)
        {
            var userTrack=await _table.FirstOrDefaultAsync(a=>a.UserName==name);
            return userTrack;
        }

        public async Task RefreshUserNofificationAsync(string name)
        {
            var userTrack = await GetTrackUserAsync(name);
            var lastTableTrack = await _context.TableTracks.LastAsync();
            if ((userTrack,lastTableTrack)!=(null,null))
            {
                    userTrack.LastUpdateSee = lastTableTrack.CreateDate;
                    await UpdateAsync(userTrack);
            }
        }
    }
}
