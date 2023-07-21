namespace Basic.Pagination;

public static class PaginatorExtension
{
    public static async Task<Paginator<T>> Page<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default
    )
    {
        return await Paginator<T>.Page(
            query,
            pageNumber,
            pageSize,
            ct
        );
    }
}