using System.Linq.Expressions;
using Basic.Context;
using Microsoft.EntityFrameworkCore;

namespace Basic.Repository;

public class GenericRepository<TEntity, TId>
    : BaseGenericRepository<TEntity>,
        IGenericRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    private readonly IGenericRepositoryHandler _genericRepositoryHandler;

    protected GenericRepository(
        IDataContext dataContext,
        IGenericRepositoryHandler genericRepositoryHandler
    ) : base(dataContext)
    {
        _genericRepositoryHandler = genericRepositoryHandler;
    }

    public async Task<TEntity?> FindByIdAsync(
        TId id,
        CancellationToken ct = default
    )
    {
        return await CurrentDbSet.FindAsync(
            new object?[]
            {
                id
            },
            ct
        );
    }

    public async Task<IEnumerable<TEntity>> FindByIdsAsync(
        IEnumerable<TId> ids,
        CancellationToken ct = default
    )
    {
        return await Queryable().Where(i => ids.Contains(i.Id)).ToListAsync(ct);
    }

    public void Update<TUpdateEntity>(
        TId id,
        TUpdateEntity updateEntity
    )
    {
        _genericRepositoryHandler.GenericUpdate(
            id,
            updateEntity,
            CurrentDbSet
        );
    }

    public void Update<TKey>(
        TId id,
        Expression<Func<TEntity, TKey>> keySelector,
        TKey value
    )
    {
        _genericRepositoryHandler.SingleValueUpdate(
            id,
            DataContext,
            CurrentDbSet,
            keySelector,
            value
        );
    }

    public void Delete(TId id)
    {
        DataContext.Remove<TId, TEntity>(id);
    }

    public void Delete(IEnumerable<TId> ids)
    {
        _genericRepositoryHandler.GenericRemoveRangeById(ids, CurrentDbSet);
    }
}