using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Helper
{
    public class CategoryHelper : ICategoryHelper
    {
        private readonly ApplicationDbContext _context;

        public CategoryHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BeatyandHealthy>> GetRelativesCategories(int numberRelated, int categoryId)
        {
           var categories= await _context.BeatyandHealthy.Where(a => a.Id != categoryId).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync();
            return categories;
        }
    }
}
