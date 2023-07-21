using Basic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Basic.Repository;

public class SeedGenericRepository<TEntity> : ISeedGenericRepository<TEntity>
    where TEntity : class
{
    protected readonly IDataContext DataContext;

    protected SeedGenericRepository(IDataContext dataContext)
    {
        DataContext = dataContext;
    }

    public DbSet<TEntity> CurrentDbSet => DataContext.Set<TEntity>();

    public IDbContextTransaction BeginTransaction()
    {
        return DataContext.BeginTransaction();
    }

    public async Task<TEntity?> FindByIdAsync(
        object id,
        CancellationToken ct = default
    )
    {
        return await CurrentDbSet.FindAsync(id, ct);
    }

    public IQueryable<TEntity> Queryable()
    {
        return CurrentDbSet.AsNoTracking();
    }
}