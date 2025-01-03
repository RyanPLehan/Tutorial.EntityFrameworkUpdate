namespace Tutorial.EntityFrameworkUpdate.Api.Models;

/// <summary>
/// It is bad practice to return just an array of items.
/// The array of items should be represented as a property.
/// The reason being, if additional information is needed at the higher level, this will be easier to append new properties
/// </summary>
public class ItemList<T>
{
    public IEnumerable<T> Items { get; init; }
}
