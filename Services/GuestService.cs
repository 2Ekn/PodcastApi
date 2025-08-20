using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Guests;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class GuestService : IGuestService
{
    private readonly PodcastDbContext _context;

    public GuestService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GuestDto>> GetAllGuestsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Guests
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new GuestDto
            {
                Id = g.Id,
                Name = g.Name,
                Bio = g.Bio,
                PhotoUrl = g.PhotoUrl
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GuestDto?> GetGuestByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var g = await _context.Guests
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (g is null)
        {
            return null;
        }

        return new GuestDto
        {
            Id = g.Id,
            Name = g.Name,
            Bio = g.Bio,
            PhotoUrl = g.PhotoUrl
        };
    }

    public async Task<GuestDto> CreateGuestAsync(CreateGuestRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        var guest = new Guest
        {
            Name = request.Name.Trim(),
            Bio = request.Bio!,
            PhotoUrl = request.PhotoUrl
        };

        _context.Guests.Add(guest);
        await _context.SaveChangesAsync(cancellationToken);

        return new GuestDto
        {
            Id = guest.Id,
            Name = guest.Name,
            Bio = guest.Bio,
            PhotoUrl = guest.PhotoUrl
        };
    }

    public async Task UpdateGuestAsync(int id, UpdateGuestRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        var guest = await _context.Guests
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        if (guest is null)
        {
            throw new KeyNotFoundException("Guest not found");
        }

        guest.Name = request.Name.Trim();
        guest.Bio = request.Bio!;
        guest.PhotoUrl = request.PhotoUrl;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGuestAsync(int id, CancellationToken cancellationToken = default)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        if (guest is null)
        {
            return;
        } 

        _context.Guests.Remove(guest);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
