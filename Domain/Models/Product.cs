namespace Tutorial.EntityFrameworkUpdate.Domain.Models;

public class Product
{
    public string Name { get; init; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
