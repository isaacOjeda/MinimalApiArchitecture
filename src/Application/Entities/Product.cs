namespace MinimalApiArchitecture.Application.Entities;
public class Product
{
    public Product(int productId, string name, string description, double price, int categoryId)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
