namespace web.Features.Cats;

using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

public class CatService : ICatService
{
    private readonly ApplicationDbContext data;

    public CatService(ApplicationDbContext data)
    {
        this.data = data;
    }

    public async Task<int> Create(string userId, CreateCatRequestModel model)
    {
        var cat = new Cat()
        {
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            UserId = userId
        };

        await this.data.AddAsync(cat);
        await this.data.SaveChangesAsync();

        return cat.Id;
    }

    public async Task<IEnumerable<CatListingResponseModel>> ByUser(string userId)
        => await this.data.Cats
            .Where(x => x.UserId == userId)
            .Select(c => new CatListingResponseModel()
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl
            })
            .AsNoTracking()
            .ToListAsync();
}