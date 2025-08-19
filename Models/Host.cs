using System.ComponentModel.DataAnnotations;

namespace TfkApi.Models;

public class Host
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Bio { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    //Navigation for EF

    public virtual ICollection<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
}
