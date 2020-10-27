
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Repository
{
    public class DiseasesRepository:BaseRepository<Disease>,IDiseasesRepository
    {
        public DiseasesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
