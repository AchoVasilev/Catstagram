namespace web.Features.Search;

using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

public class SearchController : ApiController
{
    private readonly ISearchService searchService;

    public SearchController(ISearchService searchService) 
        => this.searchService = searchService;

    [HttpGet]
    [AllowAnonymous]
    [Route(nameof(Profiles))]
    public async Task<IEnumerable<ProfileSearchModel>> Profiles(string query)
        => await this.searchService.Profiles(query);
}