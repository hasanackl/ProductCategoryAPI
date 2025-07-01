using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductCategoryAPI.Data;
using ProductCategoryAPI.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username && u.IsActive);
        if (user == null || user.Password != dto.Password)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hashString = Convert.ToBase64String(hashBytes);
        return hashString == storedHash;
    }

    private string GenerateJwtToken(ProductCategoryAPI.Models.User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
    {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("fullName", $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
    };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
    );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
