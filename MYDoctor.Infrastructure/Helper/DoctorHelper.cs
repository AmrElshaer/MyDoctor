using DocumentFormat.OpenXml.Wordprocessing;
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
        public async Task<IEnumerable<Doctor>> GetRelativesDoctors(ICollection<Doctor> doctors,int numberRelated, int? categoryId)
        {
            
                    if ((doctors.Any(), doctors.Count() >= numberRelated) == (true, true))
                            return doctors;
                     var doc=await _context.Doctor.Include(d => d.Category).Where(d=>d.CategoryId!=categoryId).OrderByDescending(a => a.Id).Take(numberRelated - doctors.Count()).ToListAsync();
                     return doctors.AppendData(doc);

      
        }
        public async Task<IEnumerable<Doctor>> GetRelativesDoctors(int numberRelated)
        {

           return await _context.Doctor.Include(rc => rc.Category).Take(numberRelated).ToListAsync();


        }
    }
}
