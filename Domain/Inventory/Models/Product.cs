namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

public class Product
{
    public int Id { get; init; } = 0;
    public string Name { get; init; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; init; }
    public IEnumerable<ProductTag> Tags { get; set; } = Enumerable.Empty<ProductTag>();
}
