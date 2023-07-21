using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace Basic.Context;

public interface IDataContext
{
    IModel Model { get; }

    DbSet<T> Set<T>()
        where T : class;

    IDbContextTransaction BeginTransaction();

    ValueTask<EntityEntry> AddAsync(
        object entity,
        CancellationToken cancellationToken = default
    );

    EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        where TEntity : class;

    EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        where TEntity : class;

    EntityEntry<TEntity> Remove<TId, TEntity>(TId id)
        where TEntity : BaseEntity<TId>;

    void Remove<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    IEnumerable<EntityEntry<TEntity>> ChangedEntries<TEntity>()
        where TEntity : class;
}