using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IHelper
{
    public interface IRelativeCategoryHelper
    {
        Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategory(ICollection<RelativeofBeatyandhealthy> relativeofBeatyandhealthies, int numberRelated, int categoryId);
    }
}
