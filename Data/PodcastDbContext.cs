using Microsoft.EntityFrameworkCore;
using PodcastApi.Models;

namespace PodcastApi.Data;

public class PodcastDbContext : DbContext
{
    public PodcastDbContext(DbContextOptions<PodcastDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Podcast> Podcasts { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Models.Host> Hosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<SocialMediaLink> SocialMediaLinks { get; set; }

    // Join entities
    public DbSet<FavoritedEpisode> FavoriteEpisodes { get; set; }
    public DbSet<ListeningHistory> ListeningHistories { get; set; }
    public DbSet<Episode2Guest> EpisodeGuests { get; set; }
    public DbSet<Episode2Tag> EpisodeTags { get; set; }
    public DbSet<Podcast2Host> PodcastHosts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Playlist2Episode> PlaylistEpisodes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --------------------------
        // Episode <-> Tag (many-to-many)
        // --------------------------
        modelBuilder.Entity<Episode2Tag>()
            .HasKey(et => new { et.EpisodeId, et.TagId });

        modelBuilder.Entity<Episode2Tag>()
            .HasOne(et => et.Episode)
            .WithMany(e => e.EpisodeTags)  // must match navigation in Episode
            .HasForeignKey(et => et.EpisodeId);

        modelBuilder.Entity<Episode2Tag>()
            .HasOne(et => et.Tag)
            .WithMany(t => t.EpisodeTags)  // must match navigation in Tag
            .HasForeignKey(et => et.TagId);

        modelBuilder.Entity<Episode2Tag>()
            .HasIndex(et => et.TagId);

        // --------------------------
        // Episode <-> Guest (many-to-many)
        // --------------------------
        modelBuilder.Entity<Episode2Guest>()
            .HasKey(eg => new { eg.EpisodeId, eg.GuestId });

        modelBuilder.Entity<Episode2Guest>()
            .HasOne(eg => eg.Episode)
            .WithMany(e => e.EpisodeGuests)
            .HasForeignKey(eg => eg.EpisodeId);

        modelBuilder.Entity<Episode2Guest>()
            .HasOne(eg => eg.Guest)
            .WithMany(g => g.EpisodeGuests)
            .HasForeignKey(eg => eg.GuestId);

        // --------------------------
        // SocialMediaLink (optional FKs to Host or Guest)
        // --------------------------
        modelBuilder.Entity<SocialMediaLink>()
            .HasOne(s => s.Host)
            .WithMany(h => h.SocialMediaLinks)
            .HasForeignKey(s => s.HostId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SocialMediaLink>()
            .HasOne(s => s.Guest)
            .WithMany(g => g.SocialMediaLinks)
            .HasForeignKey(s => s.GuestId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        // --------------------------
        // Favorite Episodes (User <-> Episode)
        // --------------------------
        modelBuilder.Entity<FavoritedEpisode>()
            .HasKey(fe => new { fe.EpisodeId, fe.UserId });

        modelBuilder.Entity<FavoritedEpisode>()
            .HasOne(fe => fe.Episode)
            .WithMany(e => e.FavoriteEpisodes) // add this nav to Episode
            .HasForeignKey(fe => fe.EpisodeId);

        modelBuilder.Entity<FavoritedEpisode>()
            .HasOne(fe => fe.User)
            .WithMany(u => u.FavoritedEpisodes)
            .HasForeignKey(fe => fe.UserId);

        modelBuilder.Entity<FavoritedEpisode>()
            .HasIndex(fe => fe.UserId);

        // --------------------------
        // Listening History
        // --------------------------
        modelBuilder.Entity<ListeningHistory>()
            .HasKey(lh => new { lh.UserId, lh.EpisodeId });

        modelBuilder.Entity<ListeningHistory>()
            .HasOne(lh => lh.User)
            .WithMany(u => u.ListeningHistories)
            .HasForeignKey(lh => lh.UserId);

        modelBuilder.Entity<ListeningHistory>()
            .HasOne(lh => lh.Episode)
            .WithMany(e => e.ListeningHistories)
            .HasForeignKey(lh => lh.EpisodeId);

        // --------------------------
        // Podcast <-> Host (many-to-many)
        // --------------------------
        modelBuilder.Entity<Podcast2Host>()
            .HasKey(ph => new { ph.PodcastId, ph.HostId });

        modelBuilder.Entity<Podcast2Host>()
            .HasOne(ph => ph.Podcast)
            .WithMany(p => p.PodcastHosts)
            .HasForeignKey(ph => ph.PodcastId);

        modelBuilder.Entity<Podcast2Host>()
            .HasOne(ph => ph.Host)
            .WithMany(h => h.PodcastHosts)
            .HasForeignKey(ph => ph.HostId);

        // --------------------------
        // Comments
        // --------------------------
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Episode)
            .WithMany(e => e.Comments)
            .HasForeignKey(c => c.EpisodeId);

        // --------------------------
        // Playlists <-> Episodes (many-to-many with order)
        // --------------------------
        modelBuilder.Entity<Playlist2Episode>()
            .HasKey(pe => new { pe.PlaylistId, pe.EpisodeId });

        modelBuilder.Entity<Playlist2Episode>()
            .HasOne(pe => pe.Playlist)
            .WithMany(p => p.PlaylistEpisodes)
            .HasForeignKey(pe => pe.PlaylistId);

        modelBuilder.Entity<Playlist2Episode>()
            .HasOne(pe => pe.Episode)
            .WithMany(e => e.PlaylistEpisodes)
            .HasForeignKey(pe => pe.EpisodeId);

        modelBuilder.Entity<Playlist>()
            .HasOne(p => p.User)
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.UserId);
        // --------------------------
        // Episode <-> Podcast (many-to-one)
        // --------------------------
        modelBuilder.Entity<Episode>()
            .HasOne(e => e.Podcast)
            .WithMany(p => p.Episodes)
            .HasForeignKey(e => e.PodcastId)
            .OnDelete(DeleteBehavior.Cascade);

    }

}
