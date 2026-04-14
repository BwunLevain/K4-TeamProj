using Microsoft.AspNetCore.Identity;

namespace UserAPI.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}