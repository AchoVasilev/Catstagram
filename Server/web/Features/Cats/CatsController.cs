namespace web.Features.Cats;

using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Infrastructure;

public class CatsController : ApiController
{
    private readonly ICatService catService;

    public CatsController(ICatService catService) 
        => this.catService = catService;

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
    {
        var userId = this.User.GetId();

        var catId = await this.catService.Create(userId, model);

        return this.Created(nameof(this.Create), catId);
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<CatListingResponseModel>> Mine()
    {
        var userId = this.User.GetId();
        var cats = await this.catService.ByUser(userId);

        return cats;
    }
}