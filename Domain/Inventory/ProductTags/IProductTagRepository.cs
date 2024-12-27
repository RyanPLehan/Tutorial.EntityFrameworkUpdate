using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags;

public interface IProductTagRepository
{
    public Task<ProductTag> Add(ProductTag productTag, CancellationToken cancellationToken = default(CancellationToken));

    public Task Delete(ProductTag productTag, CancellationToken cancellationToken = default(CancellationToken));
    public Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken));
    public Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken));
    public Task DeleteByProduct(int productId, CancellationToken cancellationToken = default(CancellationToken));

    public Task<ImmutableArray<ProductTag>> FindByName(string name, CancellationToken cancellationToken = default(CancellationToken));
    public Task<ImmutableArray<ProductTag>> FindByProduct(int productId, CancellationToken cancellationToken = default(CancellationToken));
}
