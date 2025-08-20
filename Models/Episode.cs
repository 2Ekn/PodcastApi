using System.ComponentModel.DataAnnotations;

namespace PodcastApi.Models;

public class Episode
{
    public int Id { get; set; }
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
    public int PodcastId { get; set; } 
    public Podcast Podcast { get; set; } = new Podcast();

    //Navigation for EF
    public virtual ICollection<Episode2Guest> EpisodeGuests { get; set; } = new List<Episode2Guest>();
    public virtual ICollection<Episode2Tag> EpisodeTags { get; set; } = new List<Episode2Tag>();
    public ICollection<FavoritedEpisode> FavoriteEpisodes { get; set; } = new List<FavoritedEpisode>();
    public ICollection<ListeningHistory> ListeningHistories { get; set; } = new List<ListeningHistory>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Playlist2Episode> PlaylistEpisodes { get; set; } = new List<Playlist2Episode>();
}
