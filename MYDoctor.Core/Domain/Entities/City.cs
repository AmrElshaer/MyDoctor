

using MYDoctor.Core.Domain.Common;

namespace MYDoctor.Core.Domain.Entities
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
