namespace TfkApi.DTOs;

public class PodcastDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ArtworkUrl { get; set; }
    public string? Website { get; set; }
    public List<string> Hosts { get; set; } = new();
    public int TotalEpisodes { get; set; }
    public DateTime? LatestEpisodeDate { get; set; }
}
