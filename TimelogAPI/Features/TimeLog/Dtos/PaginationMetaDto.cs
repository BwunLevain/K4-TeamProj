namespace TimelogAPI.Features.TimeLog.Dtos;

public record PaginationMetaDto(
    int Page,
    int PageSize,
    int TotalPages,
    int TotalCount,
    bool HasPreviousPage,
    bool HasNextPage
);