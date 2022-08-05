namespace web.Features.Profiles;

using Infrastructure.Services;
using Models;

public interface IProfileService
{
    Task<ProfileModel> ById(string userId, bool allInformation = false);
    
    Task<Result> Update(string userId, UpdateProfileModel model);

    Task<bool> IsPrivate(string userId);
}