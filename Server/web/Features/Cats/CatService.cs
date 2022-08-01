namespace web.Features.Cats;

using Data;
using Data.Models;

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
}