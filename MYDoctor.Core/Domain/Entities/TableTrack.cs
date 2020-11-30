using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Domain.Entities
{
    public class TableTrack:BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int ItemId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
