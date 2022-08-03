namespace web.Features.Cats;

using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Models;

public class CatService : ICatService
{
    private readonly ApplicationDbContext data;

    public CatService(ApplicationDbContext data)
    {
        this.data = data;
    }

    public async Task<int> Create(string userId, CreateCatModel model)
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

    public async Task<bool> Update(int id, string description, string userId)
    {
        var cat = await this.GetCatByIdAndUserId(id, userId);

        if (cat is null)
        {
            return false;
        }

        cat.Description = description;

        await this.data.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id, string userId)
    {
        var cat = await this.GetCatByIdAndUserId(id, userId);
        if (cat is null)
        {
            return false;
        }

        this.data.Cats.Remove(cat);
        await this.data.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CatListingModel>> ByUser(string userId)
        => await this.data.Cats
            .Where(x => x.UserId == userId)
            .Select(c => new CatListingModel()
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl
            })
            .AsNoTracking()
            .ToListAsync();

    public async Task<CatDetailsModel> Details(int catId)
        => await this.data.Cats
            .Where(x => x.Id == catId)
            .Select(x => new CatDetailsModel()
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                UserId = x.UserId,
                UserName = x.User.UserName
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    
    private async Task<Cat?> GetCatByIdAndUserId(int id, string userId) 
        => await this.data.Cats
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
}