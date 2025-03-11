using System.Linq.Expressions;

namespace InvBook.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> DeleteById(int id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        void DeleteRangeAndSaveChanges(IEnumerable<T> entities);
        Task SaveChangesAsync();
    }
}
