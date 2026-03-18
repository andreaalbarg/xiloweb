using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using XiloWeb.Api.DTOs;

namespace XiloWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration config) : ControllerBase
{
    /// <summary>Admin login — returns JWT token</summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var validUser = config["Admin:Username"];
        var validPass = config["Admin:Password"];

        if (req.Username != validUser || req.Password != validPass)
            return Unauthorized(new { message = "Invalid credentials" });

        var key     = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds   = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry  = DateTime.UtcNow.AddHours(double.Parse(config["Jwt:ExpiryHours"] ?? "8"));

        var token = new JwtSecurityToken(
            issuer:             config["Jwt:Issuer"],
            audience:           config["Jwt:Audience"],
            claims:             [new Claim(ClaimTypes.Name, req.Username), new Claim(ClaimTypes.Role, "Admin")],
            expires:            expiry,
            signingCredentials: creds
        );

        return Ok(new LoginResponse
        {
            Token  = new JwtSecurityTokenHandler().WriteToken(token),
            Expiry = expiry
        });
    }
}
