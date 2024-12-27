namespace Tutorial.EntityFrameworkUpdate.Api.Models
{
    public class SelectList<T>
    {
        public IEnumerable<T> Ids { get; init; } = Enumerable.Empty<T>();
    }
}
