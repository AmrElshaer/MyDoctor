using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Common;
using MYDoctor.Infrastructure.Identity;

namespace MYDoctor.Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _table;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            this._table = _context.Set<T>();
        }

        public async Task DeleteAsync(object id)
        {
            T existing = await _table.FindAsync(id);
            _table.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            var result = _table.AsNoTracking();
            if (expression != null)
                result = result.Where(expression);
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
         
            var existing = await _table.FindAsync(id);
            return existing;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var entity = _table.AsQueryable();
            includes.ToList().ForEach(a =>entity=entity.Include(a));
            var existing = await entity.FirstOrDefaultAsync(e => e.Id == id);
            return existing;
        }

        public async Task InsertAsync(T obj)
        {
            try
            {
                await _table.AddAsync(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<T> GetFirstAsync(Expression<Func<T,bool>> predicate) {
            var resuilt = await _table.FirstOrDefaultAsync(predicate);
            return resuilt;
        }
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {

            var entity = _table.AsQueryable();
            includes.ToList().ForEach(a => entity = entity.Include(a));
            var resuilt = await entity.FirstOrDefaultAsync(predicate);
            return resuilt;
        }
        public async Task UpdateAsync(T obj)
        {
            try { _table.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
                throw;
            }


        }
    }
    
}
