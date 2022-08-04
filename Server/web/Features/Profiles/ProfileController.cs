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
}