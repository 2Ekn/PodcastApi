using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetAllTagsAsync();
    Task<Tag?> GetTagByIdAsync(int id);
    Task<Tag> CreateTagAsync(Tag tag);
    Task UpdateTagAsync(Tag tag);
    Task DeleteTagAsync(int id);
}