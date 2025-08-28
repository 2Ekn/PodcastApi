using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Users;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class UserService : IUserService
{
    private readonly PodcastDbContext _context;

    public UserService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user is null) return null;

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName
        };
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        if (user is null) return null;

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Role = user.Role,
            SubLevel = user.SubscriptionLevel
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                DisplayName = u.DisplayName
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateUserAsync(UpdateUserRequest updateUserRequest, int id, CancellationToken cancellationToken = default)
    {
        if(updateUserRequest is null)
        {
            throw new ArgumentNullException(nameof(updateUserRequest));
        }

        var user = await _context.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if(user is null)
        {
            throw new KeyNotFoundException("No user was found with that ID.");
        }

        user.DisplayName = string.IsNullOrWhiteSpace(updateUserRequest.DisplayName) ? user.DisplayName : updateUserRequest.DisplayName;
        user.Email = string.IsNullOrWhiteSpace(updateUserRequest.Email) ? user.Email : updateUserRequest.Email;

        var emailTaken = await _context.Users
            .AnyAsync(u => u.Email == updateUserRequest.Email);

        if(emailTaken)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        int rowsAff = await _context.SaveChangesAsync();
        return rowsAff > 0;
    }
}
