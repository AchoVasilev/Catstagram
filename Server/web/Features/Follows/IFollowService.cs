namespace web.Controllers.Follows;

using Infrastructure.Services;

public interface IFollowService
{
    Task<Result> Follow(string userId, string followerId);

    Task<bool> IsFollower(string userId, string followerId);
}