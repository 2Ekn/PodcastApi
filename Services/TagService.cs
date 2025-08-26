using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Tags;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class TagService : ITagService
{
    private readonly PodcastDbContext _context;

    public TagService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<TagDto> CreateTagAsync(CreateTagRequest request, CancellationToken cancellationToken = default)
    {
        var tag = new Tag
        {
            Name = request.Name,
            Color = request.Color
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }

    public async Task<IEnumerable<TagDto>> GetAllTagsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tags
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<TagDto>> GetTagsByEpisodeIdAsync(int episodeId, CancellationToken cancellationToken = default)
    {
        return await _context.EpisodeTags
            .Where(et => et.EpisodeId == episodeId)
            .Select(et => new TagDto
            {
                Id = et.Tag.Id,
                Name = et.Tag.Name
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<TagDto?> GetTagByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var tag = await _context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (tag is null)
            return null;

        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }

    public async Task UpdateTagAsync(int id, CreateTagRequest request, CancellationToken cancellationToken = default)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (tag is null)
            throw new KeyNotFoundException($"Tag with id {id} not found");

        tag.Name = request.Name;
        tag.Color = request.Color;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTagAsync(int id, CancellationToken cancellationToken = default)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (tag is null)
            throw new KeyNotFoundException($"Tag with id {id} not found");

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
