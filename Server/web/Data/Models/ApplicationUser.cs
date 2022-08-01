namespace web.Data.Models;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
}