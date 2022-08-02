namespace web.Features.Cats;

public interface ICatService
{ 
    Task<int> Create(string userId, CreateCatRequestModel model);
}