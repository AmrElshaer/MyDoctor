using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;

namespace MyDoctor.Infrastructure
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
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
            T existing =await _table.FindAsync(id);
            _table.Remove(existing);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression< Func<T,bool>> expression=null)
        {
            if (expression!=null)return await _table.AsNoTracking().Where(expression).ToListAsync();
            return await _table.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var existing =  await _table.FindAsync(id);
            return existing;
        }

        public async Task InsertAsync(T obj)
        {
            await _table.AddAsync(obj);
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
