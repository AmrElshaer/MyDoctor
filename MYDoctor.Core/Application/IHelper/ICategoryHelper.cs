using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IHelper
{
    public interface ICategoryHelper
    {
        Task<IEnumerable<BeatyandHealthy>> GetRelativesCategories(int numberRelated, int? categoryId);
        Task<IEnumerable<BeatyandHealthy>> GetRelativesCategories(int numberRelated);
    }
}
