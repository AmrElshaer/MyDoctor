using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IHelper
{
    public interface IDoctorHelper
    {
        Task<IEnumerable<Doctor>> GetRelativesDoctors(ICollection<Doctor> doctors, int numberRelated, Expression<Func<Doctor, bool>> expression);
    }
}
