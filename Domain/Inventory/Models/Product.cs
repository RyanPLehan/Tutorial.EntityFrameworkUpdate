namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

public class Product
{
    public int Id { get; init; } = 0;
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public int CategoryId { get; init; }
    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
}
