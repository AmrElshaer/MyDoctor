using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;



namespace MyDoctor.Infrastructure
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

        public  IQueryable<T> GetAll(Expression<Func<T, bool>> expression=null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy=null,IList<Expression<Func<T, object>>> includes=null)
        {
            var result = _table.AsNoTracking();
            if (includes != null)
                includes.ToList().ForEach(a =>result= result.Include(a));
            if (expression!=null)
                 result= result.Where(expression);
            if (orderBy != null)
                result = orderBy(result);
            
            return  result;
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
                Console.WriteLine(e);
                throw;
            }

        }



        public async Task Update(T obj)
        {
            try { _table.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
    
}
