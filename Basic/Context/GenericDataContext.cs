using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Basic.Context;

public class GenericDataContext
    : DbContext,
        IDataContext
{
    protected GenericDataContext(DbContextOptions options) : base(options)
    {
    }

    public IDbContextTransaction BeginTransaction()
    {
        return Database.BeginTransaction();
    }

    public EntityEntry<TEntity> Remove<TId, TEntity>(TId id)
        where TEntity : BaseEntity<TId>
    {
        var entityToDelete = Activator.CreateInstance<TEntity>();
        entityToDelete.Id = id;
        Attach(entityToDelete);
        return Remove(entityToDelete);
    }

    public void Remove<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        RemoveRange(entities);
    }

    public IEnumerable<EntityEntry<TEntity>> ChangedEntries<TEntity>()
        where TEntity : class
    {
        return ChangeTracker.Entries<TEntity>();
    }
}