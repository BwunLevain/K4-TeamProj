namespace TimelogAPI.Features.TimeLog.Dtos;

public record PagedResponseDto<T> ( 
    IEnumerable<T> Data, 
    PaginationMetaDto Pagination
    );