namespace Tutorial.EntityFrameworkUpdate.Domain.Models;

public class Tag
{
    public int Id { get; init; } = 0;
    public string Name { get; init; }
    public string? Value { get; init; } = null;
}
