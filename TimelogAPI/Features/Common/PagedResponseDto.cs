namespace TimelogAPI.Features.Common;

public record PagedResponseDto<T> ( 
    IEnumerable<T> Data, 
    PaginationMetaDto Pagination
    );