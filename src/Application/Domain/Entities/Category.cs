namespace MinimalApiArchitecture.Application.Domain.Entities;

public class Category
{
    public Category(int categoryId, string name)
    {
        CategoryId = categoryId;
        Name = name;
    }

    public int CategoryId { get; set; }
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } =
        new HashSet<Product>();
}