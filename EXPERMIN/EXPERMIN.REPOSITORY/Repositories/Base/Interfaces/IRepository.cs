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
        void Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task<T> Add(T entity);
        void Attach(T entity);
        Task SaveChangesAsync();
        Task AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
        IQueryable<T> GetAsQueryable();
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action);
        Task<T> ExecuteInTransactionWithSaveAsync<T>(Func<Task<T>> action);
    }
}
