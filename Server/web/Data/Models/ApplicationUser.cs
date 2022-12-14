namespace web.Data.Models;

using Base;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser, IEntity
{
    public string ProfileId { get; set; }
    
    public Profile Profile { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public string CreatedBy { get; set; }
    
    public DateTime? ModifiedOn { get; set; }
    
    public string ModifiedBy { get; set; }

    public IEnumerable<Follow> Follows { get; } = new HashSet<Follow>();
    
    public IEnumerable<Follow> Followed { get; } = new HashSet<Follow>();

    public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
}