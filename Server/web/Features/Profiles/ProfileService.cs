namespace web.Features.Profiles;

using Data;
using Data.Models;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Models;

public class ProfileService : IProfileService
{
    private readonly ApplicationDbContext data;

    public ProfileService(ApplicationDbContext data)
        => this.data = data;

    public async Task<ProfileModel> ById(string userId)
        => await this.data.Users
            .Where(x => x.Id == userId)
            .Select(x => new ProfileModel()
            {
                Biography = x.Profile.Biography,
                Gender = x.Profile.Gender.ToString(),
                IsPrivate = x.Profile.IsPrivate,
                Name = x.Profile.Name,
                ProfilePhotoUrl = x.Profile.ProfilePhotoUrl,
                UserName = x.UserName,
                WebSite = x.Profile.WebSite
            })
            .FirstOrDefaultAsync();

    public async Task<Result> Update(string userId, UpdateProfileModel model)
    {
        var user = await this.data.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
        {
            return "User does not exist.";
        }

        var result = await this.ChangeProfileEmail(userId, model, user);
        if (!result.Succeeded)
        {
            return false;
        }

        result = await this.ChangeProfileUserName(userId, model, user);
        if (!result.Succeeded)
        {
            return false;
        }

        this.ChangeProfile(model, user);

        await this.data.SaveChangesAsync();

        return true;
    }

    private void ChangeProfile(UpdateProfileModel model, ApplicationUser user)
    {
        if (user.Profile.Name != model.Name)
        {
            user.Profile.Name = model.Name;
        }

        if (user.Profile.ProfilePhotoUrl != model.ProfilePhotoUrl)
        {
            user.Profile.ProfilePhotoUrl = model.ProfilePhotoUrl;
        }

        if (user.Profile.WebSite != model.WebSite)
        {
            user.Profile.WebSite = model.WebSite;
        }

        if (user.Profile.IsPrivate != model.IsPrivate)
        {
            user.Profile.IsPrivate = model.IsPrivate;
        }

        var gender = Enum.Parse<Gender>(model.Gender);
        if (user.Profile.Gender != gender)
        {
            user.Profile.Gender = gender;
        }

        if (user.Profile.Biography != model.Biography)
        {
            user.Profile.Biography = model.Biography;
        }
    }

    private async Task<Result> ChangeProfileEmail(string userId, UpdateProfileModel model, ApplicationUser user)
    {
        if (!string.IsNullOrWhiteSpace(model.Email) && model.Email != user.Email)
        {
            var emailExists = await this.data.Users.AnyAsync(x => x.Id != userId && x.Email == model.Email);
            if (emailExists)
            {
                return "The provided email is already taken.";
            }

            user.Email = model.Email;
        }

        return true;
    }

    private async Task<Result> ChangeProfileUserName(string userId, UpdateProfileModel model, ApplicationUser user)
    {
        if (!string.IsNullOrWhiteSpace(model.UserName) && model.UserName != user.UserName)
        {
            var userNameExists = await this.data.Users.AnyAsync(x => x.Id != userId && x.UserName == model.UserName);
            if (userNameExists)
            {
                return "The provided username already exists.";
            }

            user.UserName = model.UserName;
        }

        return true;
    }
}