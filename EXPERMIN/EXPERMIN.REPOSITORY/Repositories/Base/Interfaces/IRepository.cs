using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.Base.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Get(Guid id);
        Task<T> Get(string id);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task<T> Add(T entity);
        Task SaveChangesAsync();
        Task AddRange(IEnumerable<T> entities);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
        IQueryable<T> GetAsQueryable();
    }
}
