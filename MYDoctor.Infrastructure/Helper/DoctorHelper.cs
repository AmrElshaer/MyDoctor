using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Helper
{
    internal  class DoctorHelper:IDoctorHelper
    {
        private readonly ApplicationDbContext _context;

        public DoctorHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Doctor>> GetRelativesDoctors(ICollection<Doctor> doctors,int numberRelated,Expression<Func<Doctor,bool>> expression){
            
                    if ((doctors.Any(), doctors.Count() >= numberRelated) == (true, true))
                            return doctors;
                     var doc=await _context.Doctor.Include(d => d.Category).Where(expression).OrderByDescending(a => a.Id).Take(numberRelated - doctors.Count()).ToListAsync();
                     return doctors.AppendData(doc);

      
        }
    }
}
