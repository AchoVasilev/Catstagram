namespace web.Features.Cats;

using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using web.Infrastructure;

[Authorize]
public class CatsController : ApiController
{
    private readonly ICatService catService;

    public CatsController(ICatService catService) 
        => this.catService = catService;

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCatModel model)
    {
        var userId = this.User.GetId();

        var catId = await this.catService.Create(userId, model);

        return this.Created(nameof(this.Create), catId);
    }

    [HttpGet]
    public async Task<IEnumerable<CatListingModel>> Mine()
    {
        var userId = this.User.GetId();
        var cats = await this.catService.ByUser(userId);

        return cats;
    }

    [HttpGet]
    public async Task<ActionResult<CatDetailsModel>> Details(int catId)
    {
        var cat = await this.catService.Details(catId);
        if (cat is null)
        {
            return this.NotFound();
        }

        return cat;
    }
}