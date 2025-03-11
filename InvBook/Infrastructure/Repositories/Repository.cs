using InvBook.Application.Interfaces;
using InvBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvBook.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null ? await _dbSet.ToListAsync() : await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<bool> DeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }

        public void DeleteRangeAndSaveChanges(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges(); // Save first before resetting identity

            // Reset AutoIncrement for SQLite
            var tableName = _context.Model.FindEntityType(typeof(T))?.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
            {
                _context.Database.ExecuteSql($"DELETE FROM sqlite_sequence WHERE name = '{tableName}';");
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) => await _dbSet.AnyAsync(predicate);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

}
