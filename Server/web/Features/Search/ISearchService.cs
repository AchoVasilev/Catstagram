namespace web.Features.Search;

using Models;

public interface ISearchService
{
    Task<IEnumerable<ProfileSearchModel>> Profiles(string query);
}