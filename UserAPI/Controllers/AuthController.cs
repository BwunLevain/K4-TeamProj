using E_CommerceApi.DTOs.Customer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Models;
using UserAPI.Services;

namespace UserAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;
    
    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
    {
        try
        {
            // validate DTO modell
            if (!ModelState.IsValid)
                return BadRequest();

            // Create a new user/customer
            var newUser = new AppUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Email,
                Email = user.Email,
            };

            // Create the user/customer in Identity
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Add role
            await _userManager.AddToRoleAsync(newUser, "Customer");
    
            return Ok(new { Message = "Registration Successful" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
    {
        // validate DTO modell
        if (!ModelState.IsValid)
            return BadRequest();
        
        // find user in database 
        var user = await _userManager.FindByEmailAsync(userDto.Email);
        if (user == null)
            return Unauthorized("Invalid Email or Password");
        
        // check so password matches
        var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
        if(!result.Succeeded)
            return Unauthorized("Invalid Email or Password");

        // generate token 
        var token = await _tokenService.GenerateJwtToken(user);
        
        // send back token
        return Ok(new {message = "Login Successful", token});
    }
}