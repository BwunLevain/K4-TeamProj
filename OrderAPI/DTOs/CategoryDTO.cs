namespace OrderAPI.Dtos
{
    public record CategoryResponse(
        int Id,
        string Name,
        string List
    );

    public record CreateCategoryRequest(
        [Required,StringLength(50)]
        string Name,
        List<TimeLogResponse> TimeLogs

    );

    public record UpdateCategoryRequest(
        string Name
        );
}