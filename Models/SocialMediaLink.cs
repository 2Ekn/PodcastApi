using System.ComponentModel.DataAnnotations;

namespace TfkApi.Models;

public class SocialMediaLink
{
    public int Id { get; set; }


    [Required]
    [MaxLength(50)]
    public string Platform { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    // Foreign key - can be used for both Hosts and Guests
    public int? HostId { get; set; }
    public virtual Host? Host { get; set; }

    public int? GuestId { get; set; }
    public virtual Guest? Guest { get; set; }
}
