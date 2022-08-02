namespace web.Features.Identity;

using System.Net;
using Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using web.Data.Models;

public class IdentityController : ApiController
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IOptions<ApplicationSettings> applicationSettings;
    private readonly IIdentityService identityService;
    public IdentityController(
        UserManager<ApplicationUser> userManager,
        IOptions<ApplicationSettings> applicationSettings, 
        IIdentityService identityService)
    {
        this.userManager = userManager;
        this.applicationSettings = applicationSettings;
        this.identityService = identityService;
    }

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<IActionResult> Register(RegisterUserRequestModel model)
    {
        var user = new ApplicationUser()
        {
            Email = model.Email,
            UserName = model.UserName
        };

        var result = await this.userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return this.StatusCode((int)HttpStatusCode.Created);
        }

        return this.BadRequest(result.Errors);
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<object>> Login(LoginUserRequestModel model)
    {
        var user = await this.userManager.FindByNameAsync(model.UserName);
        if (user is null)
        {
            return this.Unauthorized();
        }

        var validPassword = await this.userManager.CheckPasswordAsync(user, model.Password);
        if (!validPassword)
        {
            return this.Unauthorized();
        }

        var appSettings = this.applicationSettings.Value.Secret;
        var encryptedToken = this.identityService.GenerateJwtToken(user.Id, user.UserName, appSettings);

        return new LoginResponseModel() { Token = encryptedToken };
    }
}