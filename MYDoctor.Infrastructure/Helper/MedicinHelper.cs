using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common;
using MYDoctor.Infrastructure.Identity;

namespace MYDoctor.Infrastructure.Helper
{
    internal class MedicinHelper : IMedicinHelper
    {
        private readonly ApplicationDbContext _context;

        public MedicinHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicin>> GetRelativesMedicins(ICollection<Medicin> medicins, int numberRelated, int categoryId)
        {
            if ((medicins.Any(), medicins.Count() >= numberRelated) == (true, true))
                return medicins;
            var meds = await _context.Medicin.Include(d => d.BeatyandHealthy).Where(m=>m.BeatyandHealthyId!=categoryId).OrderByDescending(m => m.Id).Take(numberRelated - medicins.Count()).ToListAsync();
            return medicins.AppendData(meds);
        }
    }
}
