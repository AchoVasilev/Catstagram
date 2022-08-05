namespace web.Features.Profiles;

using Controllers;
using Controllers.Follows;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

using static Infrastructure.WebConstants;
public class ProfileController : ApiController
{
    private readonly IProfileService profileService;
    private readonly IFollowService followService;
    private readonly ICurrentUserService userService;
    
    public ProfileController(IProfileService profileService, IFollowService followService, ICurrentUserService userService)
    {
        this.profileService = profileService;
        this.followService = followService;
        this.userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ProfileModel>> Profile()
        => await this.profileService.ById(this.userService.GetUserId(), allInformation: true);

    [HttpGet]
    [Route(RouteIdTemplate)]
    public async Task<ProfileModel> Details(string id)
    {
        var currentUserId = this.userService.GetUserId();
        var includeAllInformation = await this.followService.IsFollower(id, currentUserId);

        if (!includeAllInformation)
        {
            includeAllInformation = !await this.profileService.IsPrivate(id);
        }
        
        return await this.profileService.ById(id, includeAllInformation);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateProfileModel model)
    {
        var userId = this.userService.GetUserId();

        var result = await this.profileService.Update(userId, model);
        if (!result.Succeeded)
        {
            return this.BadRequest(result.Error);
        }

        return this.Ok();
    }
}