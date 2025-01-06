namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Models;

public class PerformantProductUpdate
{
    public required int Id { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}
