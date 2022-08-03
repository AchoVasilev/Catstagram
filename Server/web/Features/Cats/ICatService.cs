namespace web.Features.Cats;

using Models;

public interface ICatService
{ 
    Task<int> Create(string userId, CreateCatModel model);

    Task<bool> Update(int id, string description, string userId);

    Task<bool> Delete(int id, string userId);

    Task<IEnumerable<CatListingModel>> ByUser(string userId);

    Task<CatDetailsModel> Details(int catId);
}