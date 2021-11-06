namespace MinimalApiArchitecture.Api.Entities;
public class Product
{
    public Product(int productId, string name, string description, double price)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
    }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
