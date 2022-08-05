namespace web.Controllers.Follows;

using Data;
using Data.Models;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

public class FollowService : IFollowService
{
    private readonly ApplicationDbContext data;

    public FollowService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<Result> Follow(string userId, string followerId)
    {
        var userAlreadyFollowed = await this.data.Follows
            .AnyAsync(f => f.UserId == userId && f.FollowerId == followerId);

        if (userAlreadyFollowed)
        {
            return "This user is already followed.";
        }
        
        var publicProfile = await this.data.Profiles
            .Where(p => p.UserId == userId)
            .Select(p => p.IsPrivate)
            .FirstOrDefaultAsync();

        await this.data.Follows.AddAsync(new Follow()
        {
            UserId = userId,
            FollowerId = followerId,
            IsApproved = publicProfile
        });

        await this.data.SaveChangesAsync();

        return true;
    }
}