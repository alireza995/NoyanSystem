using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Basic.Repository;

public interface IBaseGenericRepository<TEntity> : ISeedGenericRepository<TEntity>
    where TEntity : class
{
    Task<EntityEntry<TEntity>> Add(
        TEntity entity,
        CancellationToken ct = default
    );

    Task Add(
        IEnumerable<TEntity> entity,
        CancellationToken ct = default
    );

    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntity> entities);

    Task<bool> SaveChangesAsync(CancellationToken ct = default);
}