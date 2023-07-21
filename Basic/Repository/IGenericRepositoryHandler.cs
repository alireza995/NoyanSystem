using System.Linq.Expressions;
using Basic.Context;
using Microsoft.EntityFrameworkCore;

namespace Basic.Repository;

public interface IGenericRepositoryHandler
{
    void GenericUpdate<TUpdateEntity, TEntity, TId>(
        TId id,
        TUpdateEntity updateEntity,
        DbSet<TEntity> dbSet
    )
        where TEntity : BaseEntity<TId>
        where TId : struct;

    void SingleValueUpdate<TKey, TEntity, TId>(
        TId id,
        IDataContext dataContext,
        DbSet<TEntity> dbSet,
        Expression<Func<TEntity, TKey>> keySelector,
        TKey value
    )
        where TEntity : BaseEntity<TId>
        where TId : struct;

    void GenericRemoveRangeById<TId, TEntity>(
        IEnumerable<TId> ids,
        DbSet<TEntity> dbSet
    )
        where TEntity : BaseEntity<TId>
        where TId : struct;
}