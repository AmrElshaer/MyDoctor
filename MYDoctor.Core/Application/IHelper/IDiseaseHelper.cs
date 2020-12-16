using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IHelper
{
    public interface IDiseaseHelper
    {
         Task<IEnumerable<Disease>> GetRelativesDiseases(ICollection<Disease> diseases, int numberRelated, Expression<Func<Disease, bool>> expression);
    }
}
