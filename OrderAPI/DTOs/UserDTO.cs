using System.ComponentModel.DataAnnotations;

namespace OrderAPI.DTOs
{
    public record UserResponse(
        int Id,
        string UserName
    );

    public record CreateUserRequest(
        [Required,StringLength(50)]
        string UserName,
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$",
        ErrorMessage = "Password must contain at least one letter and one number.")]
        string Password
    );

    public record UpdateUserRequest(
        int Id,
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$",
        ErrorMessage = "Password must contain at least one letter and one number.")]
        string Password
        );
}
