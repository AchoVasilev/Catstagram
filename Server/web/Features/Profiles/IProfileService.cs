namespace web.Features.Profiles;

using Models;

public interface IProfileService
{
    Task<ProfileModel> ById(string userId);
}