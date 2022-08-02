namespace web.Features.Cats;

public interface ICatService
{ 
    Task<int> Create(string userId, CreateCatRequestModel model);

    Task<IEnumerable<CatListingResponseModel>> ByUser(string userId);
}