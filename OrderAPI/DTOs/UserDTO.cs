namespace OrderAPI.Dtos
{
    public record UserResponse(
        int Id,
        string UserName
    );

    public record CreateUserRequest(
        [Required,StringLength(50)]
        string UserName,
        [Required]
        var Password
    );

    public record UpdateUserRequest(
        int Id,
        var Password
        );
}