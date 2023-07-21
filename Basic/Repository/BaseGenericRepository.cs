using Basic.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Basic.Repository;

public class BaseGenericRepository<TEntity>
    : SeedGenericRepository<TEntity>,
        IBaseGenericRepository<TEntity>
    where TEntity : class
{
    protected BaseGenericRepository(IDataContext dataContext) : base(dataContext)
    {
    }

    public async Task<EntityEntry<TEntity>> Add(
        TEntity entity,
        CancellationToken ct = default
    )
    {
        return await CurrentDbSet.AddAsync(entity, ct);
    }

    public async Task Add(
        IEnumerable<TEntity> entity,
        CancellationToken ct = default
    )
    {
        await CurrentDbSet.AddRangeAsync(entity, ct);
    }

    public void Delete(TEntity entity)
    {
        DataContext.Remove(entity);
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        DataContext.Remove(entities);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct = default)
    {
        return await DataContext.SaveChangesAsync(ct) > 0;
    }
}