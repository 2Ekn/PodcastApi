using System.ComponentModel.DataAnnotations;

namespace TfkApi.Models;

public class Episode
{
    public string Id { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTime PublishDate { get; set; }
    public int DurationSeconds { get; set; }
    public int EpisodeNumber { get; set; }

    [Required]
    public string AudioUrl { get; set; } = string.Empty;

    //Shownotes for the episode like tags in "Avsnittsguiden".
    //HTML for timestamps, topic, and link attached(alternative on the topic name)
    public string ShowNotesHtml { get; set; } = string.Empty;
    public bool IsPublished { get; set; } = true;


    //Foreign keys
    public string PodcastId { get; set; } = string.Empty;
    public Podcast Podcast { get; set; } = new Podcast();

    //Navigation for EF

    public virtual ICollection<Episode2Guest> EpisodeGuests { get; set; } = new List<Episode2Guest>();
    public virtual ICollection<Episode2Tag> EpisodeTags { get; set; } = new List<Episode2Tag>();
}
