namespace web.Features.Profiles;

using Controllers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

public class ProfileController : ApiController
{
    private readonly IProfileService profileService;
    private readonly ICurrentUserService userService;
    
    public ProfileController(IProfileService profileService, ICurrentUserService userService)
    {
        this.profileService = profileService;
        this.userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ProfileModel>> Profile()
        => await this.profileService.ById(this.userService.GetUserId());

    [HttpPut]
    [Authorize]
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