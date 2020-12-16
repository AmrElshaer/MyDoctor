using MYDoctor.Core.Domain.Common;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace MYDoctor.Core.Application.IRepository
{
    public interface IRepository<T> where  T:BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression=null,Func<IQueryable<T>, IOrderedQueryable<T>> orderBy=null,params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression=null,Func<IQueryable<T>, IOrderedQueryable<T>> orderBy=null);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(object id);
       
    }
}
