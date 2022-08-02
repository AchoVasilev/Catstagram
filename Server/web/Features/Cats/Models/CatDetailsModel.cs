namespace web.Features.Cats.Models;

public class CatDetailsModel : CatListingModel
{
    public string Description { get; set; }
    
    public string UserId { get; set; }
    
    public string UserName { get; set; }
}