namespace web.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Identity;

public class IdentityController : ApiController
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IOptions<ApplicationSettings> applicationSettings;

    public IdentityController(UserManager<ApplicationUser> userManager,
        IOptions<ApplicationSettings> applicationSettings)
    {
        this.userManager = userManager;
        this.applicationSettings = applicationSettings;
    }

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

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(this.applicationSettings.Value.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encryptedToken = tokenHandler.WriteToken(token);

        return new { Token = encryptedToken };
    }
}