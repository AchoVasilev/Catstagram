namespace web.Features.Cats.Models;

using System.ComponentModel.DataAnnotations;
using static Data.Validation.Cat;

public class CreateCatModel
{
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; }
    
    [Required]
    public string ImageUrl { get; set; }
}