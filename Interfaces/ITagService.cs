using PodcastApi.DTOs.Tags;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface ITagService
{
    Task<IEnumerable<TagDto>> GetAllTagsAsync(CancellationToken cancellationToken = default);
    Task<TagDto?> GetTagByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TagDto> CreateTagAsync(CreateTagRequest request, CancellationToken cancellationToken = default);
    Task UpdateTagAsync(int id, CreateTagRequest request, CancellationToken cancellationToken = default);
    Task DeleteTagAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TagDto>> GetTagsByEpisodeIdAsync(int episodeId, CancellationToken cancellationToken = default);
}