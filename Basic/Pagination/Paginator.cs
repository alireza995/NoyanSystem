using Microsoft.EntityFrameworkCore;

namespace Basic.Pagination;

public class Paginator<T>
{
    private readonly IQueryable<T> _query;

    private Paginator(IQueryable<T> query)
    {
        _query = query;
    }

    public List<T> Data { get; private set; } = null!;
    public double TotalPagesCount { get; private set; }
    public long TotalRows { get; private set; }

    public bool IsFull => Data != null! && Data.Any();

    private async Task Page(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default
    )
    {
        TotalRows = await _query.LongCountAsync(ct);
        TotalPagesCount = Math.Round(TotalRows / (double)pageSize, MidpointRounding.ToPositiveInfinity);
        Data = await _query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
    }

    public static async Task<Paginator<TQuery>> Page<TQuery>(
        IQueryable<TQuery> query,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default
    )
    {
        var result = new Paginator<TQuery>(query);
        await result.Page(
            pageNumber,
            pageSize,
            ct
        );
        return result;
    }
}