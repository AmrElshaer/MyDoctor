using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class LikeandDislikeclass
    {
        public int Id { get; set; }
        public int Like { get; set; } = 0;
        public int DisLike { get; set; } = 0;
        public string DiseaseName { get; set; }
    }
}
