using MYDoctor.Core.Domain.Common;
using System;
using System.Collections.Generic;
namespace MYDoctor.Core.Domain.Entities
{
    public class Disease:BaseEntity
    {
        public string DiseaseName { get; set; }
        public string Subject { get; set; }
        public string Image { get; set; }
        public string Protection { get; set; }
        public string Reasons { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? BeatyandHealthyId { get; set; }
        public ICollection<DiseaseMedicin> DiseaseMedicins  { get; set; }
        public BeatyandHealthy BeatyandHealthy { get; set; }

    }
}
