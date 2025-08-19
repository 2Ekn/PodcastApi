using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PodcastApi.Data;
using PodcastApi.Interfaces;
using PodcastApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PodcastApi.Services;

public class AuthService : IAuthService
{
    private readonly PodcastApiDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AuthService(PodcastApiDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Email and password must be provided");
        }
        // Find the user by email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        // If user is null or password does not match, throw an exception
        if (user == null || !VerifyPassword(password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // If user is found and password matches, generate JWT token
        return GenerateJwtToken(user);
    }

    public async Task<User> RegisterAsync(string email, string password, string displayName)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(displayName))
        {
            throw new ArgumentException("Email, password, and display name must be provided");
        }
        // Check if the user already exists
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        // If user already exists, throw an exception
        if (existingUser != null)
        {
            throw new InvalidOperationException("User already exists");
        }

        //Create a new user
        var user = new User
        {
            Email = email,
            Password = HashPassword(password),
            DisplayName = displayName
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
