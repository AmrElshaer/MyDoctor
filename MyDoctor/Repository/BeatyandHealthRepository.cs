using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Data;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Repository
{
    public class BeatyandHealthRepository:BaseRepository<BeatyandHealthy>,IBeatyandHealthRepository
    {
        public BeatyandHealthRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
