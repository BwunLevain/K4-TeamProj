using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TimelogAPI.Features.Auth;

/// <summary>
/// Hanterar autentisering och utfärdande av säkerhetstoken (JWT).
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    /// <summary>
    /// Initierar en ny instans av <see cref="AuthController"/>.
    /// </summary>
    /// <param name="config">Applikationens konfiguration för att hämta JWT-inställningar.</param>
    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Verifierar användaruppgifter och genererar en JWT-token.
    /// </summary>
    /// <param name="request">Objekt som innehåller användarnamn och lösenord.</param>
    /// <returns>En giltig Bearer-token vid lyckad inloggning.</returns>
    /// <response code="200">Inloggningen lyckades och en token har skapats.</response>
    /// <response code="401">Felaktigt användarnamn eller lösenord.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] TimelogAPI.Features.Auth.LoginRequest request)
    {
        if (request.Username != "admin" || request.Password != "password123")
        {
            return Unauthorized("Felaktigt användarnamn eller lösenord.");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new System.Security.Claims.Claim("admin", "true"),
        new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, request.Username)
    };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { Token = tokenString });
    }
}