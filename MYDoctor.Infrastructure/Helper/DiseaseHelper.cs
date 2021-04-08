using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Core.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYDoctor.Infrastructure.Identity;
namespace MYDoctor.Infrastructure.Helper
{
    internal class DiseaseHelper : IDiseaseHelper
    {
        private readonly ApplicationDbContext _context;

        public DiseaseHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Disease>> GetRelativesDiseases(ICollection<Disease> diseases, int numberRelated, int categoryId)
        {
            if ((diseases.Any(), diseases.Count() >= numberRelated) == (true, true))
                return diseases;
            var dis = await _context.Disease.Include(d => d.BeatyandHealthy).Where(a=>a.BeatyandHealthyId!=categoryId).OrderByDescending(o => o.Id).Take(numberRelated - diseases.Count()).ToListAsync();
            return diseases.AppendData(dis);
        }
    }
}
