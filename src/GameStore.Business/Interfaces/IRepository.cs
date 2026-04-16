using GameStore.Business.Dtos;

namespace GameStore.Business.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id); 
    Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate);
    Task<int> SaveChangesAsync();
}