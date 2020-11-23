using MYDoctor.Core.Domain.Common;
using MYDoctor.Core.Domain.Common.Enum;

namespace MYDoctor.Core.Domain.Entities
{
    public class Doctor:BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public BeatyandHealthy Category { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Others { get; set; }
        public string Email { get; set; }
        public Kind Kind { get; set; }
        public string ImagePath { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

   
}
