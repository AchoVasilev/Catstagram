namespace web.Features.Cats.Models;

using System.ComponentModel.DataAnnotations;
using static Data.Validation.Cat;
public class UpdateCatModel
{
    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; }
}