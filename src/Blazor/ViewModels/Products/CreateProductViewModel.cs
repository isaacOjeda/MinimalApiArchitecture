using System.ComponentModel.DataAnnotations;

namespace Blazor.ViewModels.Products;

public class CreateProductViewModel
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}
