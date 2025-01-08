namespace Tutorial.EntityFrameworkUpdate.Application.Models
{
    public class ProductTag : Domain.Models.Tag
    {
        public int Id { get; init; } = 0;
        public int ProductId { get; init; } = 0;
    }
}
