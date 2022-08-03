namespace web.Features.Cats;

using Controllers;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using static Infrastructure.WebConstants;

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
    [Route(RouteIdTemplate)]
    public async Task<ActionResult<CatDetailsModel>> Details(int id) 
        => await this.catService.Details(id);

    [HttpPut]
    public async Task<ActionResult> Update(UpdateCatModel cat)
    {
        var userId = this.User.GetId();
        var updated = await this.catService.Update(cat.Id, cat.Description, userId);

        if (!updated)
        {
            return this.BadRequest();
        }

        return this.Ok();
    }

    [HttpDelete]
    [Route(RouteIdTemplate)]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = this.User.GetId();

        var deleted = await this.catService.Delete(id, userId);
        if (!deleted)
        {
            return this.BadRequest();
        }

        return this.Ok();
    }
}