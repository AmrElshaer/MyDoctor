using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Domain.Entities
{
    public class Post:BaseEntity
    {
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public BeatyandHealthy Category { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        public UserProfile User { get; set; }
        public int UserProfileId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
