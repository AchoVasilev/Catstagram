namespace web.Controllers;

using Data;
using Data.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Cats;

public class CatsController : ApiController
{
    private readonly ApplicationDbContext data;

    public CatsController(ApplicationDbContext data) 
        => this.data = data;

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
    {
        var userId = this.User.GetId();
        var cat = new Cat()
        {
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            UserId = userId
        };

        await this.data.AddAsync(cat);
        await this.data.SaveChangesAsync();

        return this.Created(nameof(this.Create), cat.Id);
    }
}