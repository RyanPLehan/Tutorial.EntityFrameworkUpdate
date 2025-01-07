namespace Tutorial.EntityFrameworkUpdate.Application.Models
{
    public class ProductTag : Domain.Models.Tag
    {
        public int ProductId { get; init; }
    }
}
