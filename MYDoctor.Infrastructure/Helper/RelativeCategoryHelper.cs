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
    public class RelativeCategoryHelper : IRelativeCategoryHelper
    {
        private readonly ApplicationDbContext _context;

        public RelativeCategoryHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategory(ICollection<RelativeofBeatyandhealthy> relativeofBeatyandhealthies, int numberRelated, Expression<Func<RelativeofBeatyandhealthy, bool>> expression)
        {
            if ((relativeofBeatyandhealthies.Any(), relativeofBeatyandhealthies.Count() >= numberRelated) == (true, true))
                return relativeofBeatyandhealthies;
            var rels = await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(expression).OrderByDescending(c => c.Id).Take(numberRelated - relativeofBeatyandhealthies.Count()).ToListAsync(); ;
            return relativeofBeatyandhealthies.AppendData(rels);
        }
    }
}
