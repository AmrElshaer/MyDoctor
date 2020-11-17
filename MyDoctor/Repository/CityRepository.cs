using DocumentFormat.OpenXml.Bibliography;
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;


namespace MyDoctor.Repository
{
    public class CityRepository : BaseRepository<MyDoctor.Models.City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
