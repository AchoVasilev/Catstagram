namespace web.Features.Cats;

using Models;

public interface ICatService
{ 
    Task<int> Create(string userId, CreateCatModel model);

    Task<IEnumerable<CatListingModel>> ByUser(string userId);

    Task<CatDetailsModel> Details(int catId);
}