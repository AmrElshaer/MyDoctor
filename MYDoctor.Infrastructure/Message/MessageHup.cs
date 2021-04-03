using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Message
{
   
    public class MessageHup:Hub
    {
        private readonly IHubContext<MessageHup> _hubContext;
        IInboxMessageRepsitory _inboxMessageRepsitory;
        
        public MessageHup(IHubContext<MessageHup> hubContext,IInboxMessageRepsitory inboxMessageRepsitory)
        {
            _hubContext = hubContext;
            _inboxMessageRepsitory = inboxMessageRepsitory;

        }

        private async Task SendMissMessageAsync(string toName)
        {
            var allMessages = await _inboxMessageRepsitory.GetMissMessages(toName);
            await this._hubContext.Clients.Group(toName).SendAsync("receiveMessage", allMessages);
        }
       
        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                string name = Context.User.Identity.Name;
                Groups.AddToGroupAsync(Context.ConnectionId, name).GetAwaiter().GetResult();
                SendMissMessageAsync(name).GetAwaiter().GetResult();
            }


            return base.OnConnectedAsync();
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
