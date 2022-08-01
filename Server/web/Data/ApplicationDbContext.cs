
namespace web.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
    
    public DbSet<Cat> Cats { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Cat>()
            .HasOne(c => c.User)
            .WithMany(u => u.Cats)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        base.OnModelCreating(builder);
    }
}
