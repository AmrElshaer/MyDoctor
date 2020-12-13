using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Domain.Entities
{
    public class UserProfile:BaseEntity
    {
        public UserProfile()
        {
            InboxMessages = new HashSet<InboxMessage>();
            ToMessagesInbox = new HashSet<InboxMessage>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public ICollection<InboxMessage> InboxMessages { get; set; }
        public ICollection<InboxMessage> ToMessagesInbox { get; set; }
    }
}
