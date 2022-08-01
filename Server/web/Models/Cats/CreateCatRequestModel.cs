namespace web.Models.Cats;

using System.ComponentModel.DataAnnotations;
using static Data.Validation.Cat;

public class CreateCatRequestModel
{
    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; }
    
    [Required]
    public string ImageUrl { get; set; }
}