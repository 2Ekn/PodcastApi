using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PodcastApi.Data;
using PodcastApi.DTOs.Auth;
using PodcastApi.DTOs.Users;
using PodcastApi.Interfaces;
using PodcastApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PodcastApi.Services;

public class AuthService : IAuthService
{
    private readonly PodcastDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AuthService(PodcastDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> LoginAsync(DTOs.Auth.LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        {
            throw new ArgumentException("Email and password must be provided");
        }

        // Find the user by email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

        // If user is null or password does not match, throw an exception
        if (user == null || !VerifyPassword(loginRequest.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        //generate JWT token to found user
        return GenerateJwtToken(user);
    }

    public async Task<UserDto> RegisterAsync(DTOs.Auth.RegisterRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(registerRequest.Email) ||
            string.IsNullOrWhiteSpace(registerRequest.Password) ||
            string.IsNullOrWhiteSpace(registerRequest.DisplayName))
        {
            throw new ArgumentException("Email, password, and display name must be provided");
        }

        var normalizedEmail = registerRequest.Email.Trim().ToLowerInvariant();

        // Check if user already exists
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);

        if (existingUser != null)
        {
            throw new InvalidOperationException("User already exists");
        }

        // Create and save new user
        var user = new User
        {
            Email = normalizedEmail,
            Password = HashPassword(registerRequest.Password),
            DisplayName = registerRequest.DisplayName.Trim()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        // Return DTO, not entity
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName
        };
    }

    private AuthResponse GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.Role, user.Role)
        }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return new AuthResponse
        {
            Token = jwt,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName
            }
        };
    }


    //Helper methods to make it cleaner
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
