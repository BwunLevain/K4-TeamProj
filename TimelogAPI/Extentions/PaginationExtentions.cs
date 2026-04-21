using TimelogAPI.Features.Common;

namespace TimelogAPI.Extentions
{
    public static class PaginationExtensions
    {
        public static PagedResponseDto<TResponse> ToPagedResponse<TEntity, TResponse>(
            this IQueryable<TEntity> query,
            int page,
            int pageSize,
            Func<TEntity, TResponse> mapper)
        {
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(mapper)
                .ToList();

            var meta = new PaginationMetaDto(
                page,
                pageSize,
                totalPages,
                totalCount,
                page > 1,
                page < totalPages
            );

            return new PagedResponseDto<TResponse>(items, meta);
        }
    }
}