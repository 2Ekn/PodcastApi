using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Hosts;
using PodcastApi.Interfaces;

namespace PodcastApi.Services;

public sealed class HostService : IHostService
{
    private readonly PodcastDbContext _context;

    public HostService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HostDto>> GetAllHostsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Hosts
            .AsNoTracking()
            .OrderBy(h => h.Name)
            .Select(h => new HostDto
            {
                Id = h.Id,
                Name = h.Name,
                Bio = h.Bio,
                ImageUrl = h.ImageUrl
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<HostDto?> GetHostByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var h = await _context.Hosts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (h is null)
        {
            return null;
        }

        return new HostDto
        {
            Id = h.Id,
            Name = h.Name,
            Bio = h.Bio,
            ImageUrl = h.ImageUrl
        };
    }

    public async Task<HostDto> CreateHostAsync(CreateHostRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        var entity = new Models.Host
        {
            Name = request.Name.Trim(),
            Bio = request.Bio!,
            ImageUrl = request.ImageUrl!
        };

        _context.Hosts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new HostDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Bio = entity.Bio,
            ImageUrl = entity.ImageUrl
        };
    }

    public async Task UpdateHostAsync(int id, CreateHostRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        var entity = await _context.Hosts.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException("Host not found");
        }

        entity.Name = request.Name.Trim();
        entity.Bio = request.Bio!;
        entity.ImageUrl = request.ImageUrl!;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteHostAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Hosts.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        if (entity is null)
        {
            return;
        }

        _context.Hosts.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
