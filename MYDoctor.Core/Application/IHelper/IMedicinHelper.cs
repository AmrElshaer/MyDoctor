using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IHelper
{
    public interface IMedicinHelper
    {
        Task<IEnumerable<Medicin>> GetRelativesMedicins(ICollection<Medicin> medicins, int numberRelated, int categoryId);
    }
}
