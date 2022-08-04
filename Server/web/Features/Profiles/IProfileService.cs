namespace web.Features.Profiles;

using Infrastructure.Services;
using Models;

public interface IProfileService
{
    Task<ProfileModel> ById(string userId);
    
    Task<Result> Update(string userId, UpdateProfileModel model);
}