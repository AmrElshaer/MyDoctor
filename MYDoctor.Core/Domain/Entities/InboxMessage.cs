using MYDoctor.Core.Domain.Common;
using System;
namespace MYDoctor.Core.Domain.Entities
{
    public  class InboxMessage:BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int? ToUserProfileId { get; set; }
        public UserProfile ToUserProfile { get; set; }
        public bool IsSee { get; set; }

    }
}
