using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Domain.Entities
{
    public class Like:BaseEntity
    {
        public Post Post { get; set; }
        public int? PostId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int? UserProfileId { get; set; }
    }
}
