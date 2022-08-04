namespace web.Features.Profiles.Models;

using System.ComponentModel.DataAnnotations;
using Data.Models;
using static Data.Validation.User;

public class UpdateProfileModel
{
    public string UserName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [MaxLength(MaxNameLength)] 
    public string Name { get; set; }

    public string ProfilePhotoUrl { get; set; }

    public string WebSite { get; set; }

    [MaxLength(MaxBiographyLength)] 
    public string Biography { get; set; }

    public string Gender { get; set; }

    public bool IsPrivate { get; set; } = false;
}