namespace web.Controllers.Follows;

using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

public class FollowController : ApiController
{
    private readonly ICurrentUserService userService;
    private readonly IFollowService followService;
    
    public FollowController(ICurrentUserService userService, IFollowService followService)
    {
        this.userService = userService;
        this.followService = followService;
    }

    [HttpPost]
    public async Task<ActionResult> Follow(FollowRequestModel model)
    {
        var followerId = this.userService.GetUserId();
        var result = await this.followService.Follow(model.UserId, followerId);

        if (!result.Succeeded)
        {
            return this.BadRequest(result.Error);
        }

        return this.Ok();
    }
}