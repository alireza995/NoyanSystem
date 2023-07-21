using System.Linq.Expressions;

namespace Basic.Repository;

public interface IGenericRepository<TEntity, in TId> : IBaseGenericRepository<TEntity>
    where TEntity : BaseEntity<TId>
{
    Task<TEntity?> FindByIdAsync(
        TId id,
        CancellationToken ct = default
    );

    Task<IEnumerable<TEntity>> FindByIdsAsync(
        IEnumerable<TId> ids,
        CancellationToken ct = default
    );

    void Update<TUpdateEntity>(
        TId id,
        TUpdateEntity updateEntity
    );

    void Update<TKey>(
        TId id,
        Expression<Func<TEntity, TKey>> keySelector,
        TKey value
    );

    void Delete(TId id);
    void Delete(IEnumerable<TId> ids);
}