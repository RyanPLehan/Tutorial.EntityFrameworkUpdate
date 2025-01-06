using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Repositories;

public interface IProductRepository
{
    public Task<Product> Add(Product product, CancellationToken cancellationToken = default);

    public Task Delete(Product product, CancellationToken cancellationToken = default);
    public Task Delete(int id, CancellationToken cancellationToken = default);
    public Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    public Task DeleteByCategory(int categoryId, CancellationToken cancellationToken = default);

    public Task<Product?> Get(int id, CancellationToken cancellationToken = default);
    public Task<ImmutableArray<Product>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default);

    public Task<ImmutableArray<Product>> FindByCategory(int categoryId, CancellationToken cancellationToken = default);
    public Task<ImmutableArray<int>> FindIdByCategory(int categoryId, CancellationToken cancellationToken = default);

    public Task ReplaceCategory(int oldCategoryId, int newCategoryId, CancellationToken cancellationToken = default);

    public Task<Product?> Update(int id, string description, decimal price, int quantity, CancellationToken cancellationToken = default);
    public Task UpdateDescription(int id, string description, CancellationToken cancellationToken = default);
    public Task UpdatePrice(int id, decimal price, CancellationToken cancellationToken = default);
    public Task UpdateQuantity(int id, int quantity, CancellationToken cancellationToken = default);

    /// <summary>
    /// This will update an entity that is not tracked, but updates only specific fields.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Product?> PerformantUpdate(int id, string description, decimal price, int quantity, CancellationToken cancellationToken = default);
}
