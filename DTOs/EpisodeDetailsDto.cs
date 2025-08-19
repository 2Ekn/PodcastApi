namespace PodcastApi.DTOs;
public class EpisodeDetailDto : EpisodeDto
{
    public string? ShowNotesHtml { get; set; }
    public string? TranscriptUrl { get; set; }
    public PodcastDto Podcast { get; set; } = null!;
}
