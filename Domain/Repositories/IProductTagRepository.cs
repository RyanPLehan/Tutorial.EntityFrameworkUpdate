using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Repositories;

public interface IProductTagRepository
{
    public Task<ProductTag> Add(ProductTag productTag, CancellationToken cancellationToken = default);
    public Task<ImmutableArray<ProductTag>> Add(IEnumerable<ProductTag> productTags, CancellationToken cancellationToken = default);

    public Task Delete(ProductTag productTag, CancellationToken cancellationToken = default);
    public Task Delete(int productId, int id, CancellationToken cancellationToken = default);
    public Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    public Task DeleteByProduct(int productId, CancellationToken cancellationToken = default);
    public Task DeleteByProducts(IEnumerable<int> productIds, CancellationToken cancellationToken = default);

    public Task<ProductTag?> Get(int productId, int id, CancellationToken cancellationToken = default);

    public Task<ImmutableArray<ProductTag>> FindByName(string name, CancellationToken cancellationToken = default);
    public Task<ImmutableArray<ProductTag>> FindByProduct(int productId, CancellationToken cancellationToken = default);
}
