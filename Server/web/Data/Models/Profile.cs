namespace web.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Validation.User;

public class Profile
{
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; }
    
    public ApplicationUser User { get; set; }
    
    [MaxLength(MaxNameLength)] public string Name { get; set; }

    public string ProfilePhotoUrl { get; set; }

    public string WebSite { get; set; }

    [MaxLength(MaxBiographyLength)] public string Biography { get; set; }

    public Gender Gender { get; set; }

    public bool IsPrivate { get; set; } = false;
}