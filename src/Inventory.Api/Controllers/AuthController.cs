using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login(
        LoginRequest request)
    {
        if (
            request.Username != "admin" ||
            request.Password != "Admin123*")
        {
            return Unauthorized(
                new
                {
                    Message = "Credenciales inválidas."
                });
        }

        var claims = new[]
        {
            new Claim(
                ClaimTypes.Name,
                request.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var durationInMinutes = int.Parse(
            _configuration[
                "Jwt:DurationInMinutes"]!);

        var expires = DateTime.UtcNow.AddMinutes(
            durationInMinutes);

        var token = new JwtSecurityToken(
            issuer:
                _configuration["Jwt:Issuer"],
            audience:
                _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        var jwtToken =
            new JwtSecurityTokenHandler()
                .WriteToken(token);

        return Ok(
            new
            {
                Token = jwtToken
            });
    }
}