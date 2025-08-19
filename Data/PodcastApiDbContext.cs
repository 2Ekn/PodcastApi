using Microsoft.EntityFrameworkCore;
using TfkApi.Models;

namespace TfkApi.Data;

public class PodcastApiDbContext : DbContext
{
    public PodcastApiDbContext(DbContextOptions<PodcastApiDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // EpisodeTag many-to-many configuration
        modelBuilder.Entity<Episode2Tag>()
            .HasKey(et => new { et.EpisodeId, et.TagId });

        modelBuilder.Entity<Episode2Tag>()
            .HasOne(et => et.Episode)
            .WithMany(e => e.EpisodeTags)  // This should match your Episode model property
            .HasForeignKey(et => et.EpisodeId);

        modelBuilder.Entity<Episode2Tag>()
            .HasOne(et => et.Tag)
            .WithMany(t => t.EpisodeTags)  // This should match your Tag model property
            .HasForeignKey(et => et.TagId);

        // EpisodeGuest many-to-many configuration
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

        // SocialMediaLink configuration (optional, but good for clarity)
        modelBuilder.Entity<SocialMediaLink>()
            .HasOne(s => s.Host)
            .WithMany(h => h.SocialMediaLinks)
            .HasForeignKey(s => s.HostId);

        modelBuilder.Entity<SocialMediaLink>()
            .HasOne(s => s.Guest)
            .WithMany(g => g.SocialMediaLinks)
            .HasForeignKey(s => s.GuestId);
    }
}
