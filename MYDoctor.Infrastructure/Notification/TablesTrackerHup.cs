using Microsoft.AspNetCore.SignalR;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Notification
{
   
    public  class TablesTrackerHup:Hub,ITableTrackerHup
    {
        private readonly IHubContext<TablesTrackerHup> _hubContext;
        private readonly ITableTrackNotification _tableTrackNotification;
        private readonly ITableTrackUserRepository _tableTrackUserRepository;
        public TablesTrackerHup(IHubContext<TablesTrackerHup> hubContext, ITableTrackNotification tableTrackNotification,ITableTrackUserRepository tableTrackUserRepository)
        {
            _hubContext=hubContext;
            _tableTrackNotification = tableTrackNotification;
            _tableTrackUserRepository = tableTrackUserRepository;
        }
        public async Task TableTrackerNotifiy(IEnumerable<TableTrack> tableTracks,string name) {
            await this._hubContext.Clients.Group(name).SendAsync("notifiyTableTracker",tableTracks);
           
        }
        public  override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                string name = Context.User.Identity.Name;
                Groups.AddToGroupAsync(Context.ConnectionId, name).GetAwaiter().GetResult();
                SendTableTrackNotification(name);

            }


            return base.OnConnectedAsync();
        }

        private void SendTableTrackNotification(string name)
        {
            var userTrack = _tableTrackUserRepository.GetTrackUserAsync(name).GetAwaiter().GetResult();
            
            if (userTrack == null)
            {
                var lastTrackNotify = _tableTrackNotification.GetAll().OrderByDescending(a => a.Id).Take(4).ToList();
                _tableTrackUserRepository.InsertAsync(new TableTrackUser { UserName = name, LastUpdateSee = lastTrackNotify.FirstOrDefault() != null ? lastTrackNotify.First().CreateDate : DateTime.Now }).GetAwaiter().GetResult();
                TableTrackerNotifiy(lastTrackNotify, name).GetAwaiter().GetResult();
            }
            else
            {
                var lastTrackNotify = _tableTrackNotification.GetAll(a => a.CreateDate > userTrack.LastUpdateSee).OrderByDescending(a => a.Id).ToList();
                TableTrackerNotifiy(lastTrackNotify, name).GetAwaiter().GetResult();

            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                string name = Context.User.Identity.Name;
                Groups.RemoveFromGroupAsync(Context.ConnectionId, name);

            }
            return base.OnDisconnectedAsync(exception);
        }
    }

}
