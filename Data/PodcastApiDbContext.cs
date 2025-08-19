using Microsoft.EntityFrameworkCore;
using PodcastApi.Models;

namespace PodcastApi.Data;

public class PodcastApiDbContext : DbContext
{
    public PodcastApiDbContext(DbContextOptions<PodcastApiDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Models.Host> Hosts { get; set; }
    public DbSet<SocialMediaLink> SocialMediaLinks { get; set; }
    public DbSet<FavoritedEpisode> FavoritedEpisodes { get; set; }
    public DbSet<Episode2Tag> Episode2Tags { get; set; }
    public DbSet<Episode2Guest> Episode2Guests { get; set; }
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
            .HasForeignKey(s => s.HostId)
            .IsRequired(false); // if nullable

        modelBuilder.Entity<SocialMediaLink>()
            .HasOne(s => s.Guest)
            .WithMany(g => g.SocialMediaLinks)
            .HasForeignKey(s => s.GuestId)
            .IsRequired(false); // if nullable

        modelBuilder.Entity<FavoritedEpisode>()
            .HasKey(eu => new { eu.EpisodeId, eu.UserId });

        modelBuilder.Entity<FavoritedEpisode>()
            .HasOne(eu => eu.Episode)
            .WithMany() // or WithMany(e => e.FavoriteByUsers) if Episode has this property
            .HasForeignKey(eu => eu.EpisodeId);

        modelBuilder.Entity<FavoritedEpisode>()
            .HasOne(eu => eu.User)
            .WithMany(u => u.FavoritedEpisodes)
            .HasForeignKey(eu => eu.UserId);

        modelBuilder.Entity<FavoritedEpisode>()
            .HasIndex(fe => fe.UserId);

        modelBuilder.Entity<Episode2Tag>()
            .HasIndex(et => et.TagId);
    }
}
