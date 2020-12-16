using MYDoctor.Core.Domain.Common;
using System.Collections.Generic;
namespace MYDoctor.Core.Domain.Entities
{
    public class UserProfile:BaseEntity
    {
        public UserProfile()
        {
            InboxMessages = new HashSet<InboxMessage>();
            ToMessagesInbox = new HashSet<InboxMessage>();
            Posts = new HashSet<Post>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public ICollection<InboxMessage> InboxMessages { get; set; }
        public ICollection<InboxMessage> ToMessagesInbox { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
