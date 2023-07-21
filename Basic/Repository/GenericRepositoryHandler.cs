using System.Linq.Expressions;
using System.Reflection;
using Basic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Basic.Repository;

public class GenericRepositoryHandler : IGenericRepositoryHandler
{
    private readonly IDataContext _dataContext;

    public GenericRepositoryHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void GenericUpdate<TUpdateEntity, TEntity, TId>(
        TId id,
        TUpdateEntity updateEntity,
        DbSet<TEntity> dbSet
    )
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        var updatingEntity = CreateEntity<TId, TEntity>(id);
        var currentEntry = dbSet.Attach(updatingEntity);
        var properties = GetProperties<TId, TEntity>(updatingEntity);
        var toUpdateProperties =
            MergeSourceAndDestinationProperties<TUpdateEntity>(nameof(updatingEntity.Id), properties);

        foreach (var sourceAndDest in toUpdateProperties)
        {
            FlagModified(
                currentEntry,
                updateEntity,
                sourceAndDest.Key,
                sourceAndDest.Value
            );
        }
    }

    public void SingleValueUpdate<TKey, TEntity, TId>(
        TId id,
        IDataContext dataContext,
        DbSet<TEntity> dbSet,
        Expression<Func<TEntity, TKey>> keySelector,
        TKey value
    )
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        var updatingEntity = CreateEntity<TId, TEntity>(id);
        var currentEntry = dataContext.ChangedEntries<TEntity>().FirstOrDefault(i => i.Entity.Id.Equals(id)) ??
            dbSet.Attach(updatingEntity);

        currentEntry.Property(keySelector).CurrentValue = value;
    }

    public void GenericRemoveRangeById<TId, TEntity>(
        IEnumerable<TId> ids,
        DbSet<TEntity> dbSet
    )
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        foreach (var id in ids)
        {
            var updatingEntity = CreateEntity<TId, TEntity>(id);
            var currentEntry = dbSet.Entry(updatingEntity);

            if (currentEntry.State is EntityState.Detached)
            {
                currentEntry = dbSet.Attach(updatingEntity);
            }

            currentEntry.State = EntityState.Deleted;
        }
    }

    private TEntity CreateEntity<TId, TEntity>(TId id)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        var entity = Activator.CreateInstance<TEntity>();
        entity.Id = id;
        return entity;
    }

    private IEnumerable<IProperty> GetProperties<TId, TEntity>(TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        return _dataContext.Model.FindEntityType(entity.GetType())!.GetProperties()
           .Where(i => i.Name != nameof(entity.Id))
           .ToList();
    }

    private Dictionary<IProperty, PropertyInfo> MergeSourceAndDestinationProperties<TUpdateEntity>(
        string idFieldName,
        IEnumerable<IProperty> destinationProperties
    )
    {
        var updatingProperties = typeof(TUpdateEntity).GetProperties().Where(i => i.Name != idFieldName).ToList();

        return new Dictionary<IProperty, PropertyInfo>(
            from dest in destinationProperties join source in updatingProperties on dest.Name equals source.Name
            select new KeyValuePair<IProperty, PropertyInfo>(dest, source)
        );
    }

    private void FlagModified<TUpdateEntity>(
        EntityEntry entry,
        TUpdateEntity entity,
        IReadOnlyPropertyBase property,
        PropertyInfo propertyInfo
    )
    {
        var value = propertyInfo.GetValue(entity);

        if (value == null)
        {
            return;
        }

        entry.Property(property.Name).CurrentValue = value;
    }
}