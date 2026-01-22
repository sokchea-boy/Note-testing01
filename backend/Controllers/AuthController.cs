using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Data;
using Dapper;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IDbConnection _db;
    private readonly IConfiguration _config;

    public AuthController(IDbConnection db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public User User { get; set; }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        Console.WriteLine($"Register attempt for email: {request.Email}");
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            Console.WriteLine("Email or password missing");
            return BadRequest("Email and password are required");
        }

        var existingUser = await _db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = request.Email });
        if (existingUser != null)
        {
            Console.WriteLine("User already exists");
            return BadRequest("User already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };

        var sql = "INSERT INTO Users (Email, PasswordHash, CreatedAt) VALUES (@Email, @PasswordHash, @CreatedAt); SELECT last_insert_rowid();";
        user.Id = await _db.ExecuteScalarAsync<int>(sql, user);
        Console.WriteLine($"User created with ID: {user.Id}");

        var token = GenerateJwtToken(user);
        Console.WriteLine("Token generated successfully");
        return Ok(new AuthResponse { Token = token, User = user });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = request.Email });
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);
        return Ok(new AuthResponse { Token = token, User = user });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-for-jwt"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "backend",
            audience: "frontend",
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}