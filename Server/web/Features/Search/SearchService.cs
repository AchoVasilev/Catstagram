namespace web.Features.Search;

using Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class SearchService : ISearchService
{
    private readonly ApplicationDbContext data;

    public SearchService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<IEnumerable<ProfileSearchModel>> Profiles(string query)
        => await this.data.Users
            .Where(u => u.UserName.ToLower().Contains(query.ToLower()) ||
                        u.Profile.Name.ToLower().Contains(query.ToLower()))
            .Select(u => new ProfileSearchModel()
            {
                ProfilePhotoUrl = u.Profile.ProfilePhotoUrl,
                UserId = u.Id,
                UserName = u.UserName
            })
            .ToListAsync();
}