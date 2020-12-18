using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Domain.Entities
{
    public class Post:BaseEntity
    {
        public Post()
        {
            Likes = new HashSet<Like>();
            DisLikes = new HashSet<DisLike>();
        }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public BeatyandHealthy Category { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<DisLike> DisLikes { get; set; }
        public UserProfile User { get; set; }
        public int UserProfileId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
