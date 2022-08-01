namespace web.Controllers;

using System.Net;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Identity;

public class IdentityController : ApiController
{
    private readonly UserManager<ApplicationUser> userManager;

    public IdentityController(UserManager<ApplicationUser> userManager) 
        => this.userManager = userManager;

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
}