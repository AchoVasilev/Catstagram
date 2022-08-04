namespace web.Features.Profiles;

using Data;
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
}