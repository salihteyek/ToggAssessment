using System.Linq.Expressions;

namespace ManagementPanel.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task Remove(TEntity entity);
        Task<TEntity> Update(TEntity entity);
    }
}
