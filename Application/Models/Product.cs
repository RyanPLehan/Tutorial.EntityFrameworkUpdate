namespace Tutorial.EntityFrameworkUpdate.Application.Models;

public class Product : Domain.Models.Product
{
    public int Id { get; init; } = 0;
    public int CategoryId { get; init; }
    public IEnumerable<ProductTag> Tags { get; set; } = Enumerable.Empty<ProductTag>();
}
