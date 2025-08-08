namespace TfkApi.DTOs;

public class EpisodeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public int DurationSeconds { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public int? Season { get; set; }
    public int? EpisodeNumber { get; set; }
    public List<string> Guests { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}
