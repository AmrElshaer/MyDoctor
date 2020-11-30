using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Domain.Entities
{
    public class TableTrackUser:BaseEntity
    {
        public string UserName { get; set; }
        public DateTime LastUpdateSee { get; set; }
    }
}
