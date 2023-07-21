using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Basic.Repository;

public interface ISeedGenericRepository<TEntity>
    where TEntity : class
{
    DbSet<TEntity> CurrentDbSet { get; }
    IDbContextTransaction BeginTransaction();

    Task<TEntity?> FindByIdAsync(
        object id,
        CancellationToken ct = default
    );

    IQueryable<TEntity> Queryable();
}