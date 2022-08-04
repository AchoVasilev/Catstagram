namespace web.Features.Profiles.Models;

using web.Data.Models;

public class ProfileModel
{
    public string Name { get; set; }

    public string ProfilePhotoUrl { get; set; }

    public string WebSite { get; set; }

    public string Biography { get; set; }

    public string Gender { get; set; }

    public bool IsPrivate { get; set; }
    
    public string UserName { get; set; }
}