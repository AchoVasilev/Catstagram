namespace web.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Data.Validation.Cat;
public class Cat
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; }
    
    [Required]
    public string ImageUrl { get; set; }
    
    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    
    public ApplicationUser User { get; set; }
}