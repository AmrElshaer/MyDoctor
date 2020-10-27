using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDoctor.Infrastructure
{
    public interface IRepository<T> where  T:class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T obj);
        void Update(T obj);
        Task DeleteAsync(object id);
        Task SaveAsync();
    }
}
