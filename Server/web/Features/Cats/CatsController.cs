namespace web.Features.Cats;

using Controllers;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using static Infrastructure.WebConstants;

public class CatsController : ApiController
{
    private readonly ICatService catService;
    private readonly ICurrentUserService currentUserService;
    public CatsController(ICatService catService, ICurrentUserService currentUserService)
    {
        this.catService = catService;
        this.currentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCatModel model)
    {
        var userId = this.currentUserService.GetUserId();

        var catId = await this.catService.Create(userId, model);

        return this.Created(nameof(this.Create), catId);
    }

    [HttpGet]
    public async Task<IEnumerable<CatListingModel>> Mine()
    {
        var userId = this.currentUserService.GetUserId();
        var cats = await this.catService.ByUser(userId);

        return cats;
    }

    [HttpGet]
    [Route(RouteIdTemplate)]
    public async Task<ActionResult<CatDetailsModel>> Details(int id) 
        => await this.catService.Details(id);

    [HttpPut]
    [Route(RouteIdTemplate)]
    public async Task<ActionResult> Update(int id, UpdateCatModel cat)
    {
        var userId = this.currentUserService.GetUserId();
        var updated = await this.catService.Update(id, cat.Description, userId);

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
        var userId = this.currentUserService.GetUserId();

        var deleted = await this.catService.Delete(id, userId);
        if (!deleted)
        {
            return this.BadRequest();
        }

        return this.Ok();
    }
}