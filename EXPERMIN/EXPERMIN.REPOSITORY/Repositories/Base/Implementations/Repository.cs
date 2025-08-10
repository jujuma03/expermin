using EXPERMIN.DATABASE.Data;
using EXPERMIN.REPOSITORY.Repositories.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.Base.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ExperminContext _context;
        private readonly DbSet<T> _entity;

        public Repository(ExperminContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }
        public virtual async Task<T> Get(Guid id)
            => await _entity.FindAsync(id);

        public virtual async Task<T> Get(string id)
            => await _entity.FindAsync(id);
        public virtual async Task Insert(T entity)
        {
            await _entity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public virtual async Task InsertRange(IEnumerable<T> entities)
        {
            await _entity.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                // Should call Dispose() to remove the elements from the failed context?
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> Add(T entity)
        {
            var result = await _entity.AddAsync(entity);
            return result.Entity;
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _entity.AddRangeAsync(entities);
        }

        public async Task Delete(T entity)
        {
            _entity.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            _entity.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAsQueryable()
            => _entity.AsQueryable();
    }
}
