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

    public async Task<ProfileModel> ById(string userId, bool allInformation = false)
    {
        var query = this.data.Users
            .Where(x => x.Id == userId);

        if (allInformation)
        {
            return await query
                .Select(x => new PublicProfileModel()
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
        }

        return await query
            .Select(x => new ProfileModel()
            {
                IsPrivate = x.Profile.IsPrivate,
                Name = x.Profile.Name,
                ProfilePhotoUrl = x.Profile.ProfilePhotoUrl,
                UserName = x.UserName,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Result> Update(string userId, UpdateProfileModel model)
    {
        var user = await this.data.Users
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
        {
            return "User does not exist.";
        }

        user.Profile ??= new Profile();

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

        this.ChangeProfile(model, user.Profile);

        await this.data.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IsPrivate(string userId)
        => await this.data.Profiles
            .AnyAsync(x => x.UserId == userId && x.IsPrivate);

    private void ChangeProfile(UpdateProfileModel model, Profile profile)
    {
        if (profile.Name != model.Name)
        {
            profile.Name = model.Name;
        }

        if (profile.ProfilePhotoUrl != model.ProfilePhotoUrl)
        {
            profile.ProfilePhotoUrl = model.ProfilePhotoUrl;
        }

        if (profile.WebSite != model.WebSite)
        {
            profile.WebSite = model.WebSite;
        }

        if (profile.IsPrivate != model.IsPrivate)
        {
            profile.IsPrivate = model.IsPrivate;
        }

        var gender = Enum.Parse<Gender>(model.Gender);
        if (profile.Gender != gender)
        {
            profile.Gender = gender;
        }

        if (profile.Biography != model.Biography)
        {
            profile.Biography = model.Biography;
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