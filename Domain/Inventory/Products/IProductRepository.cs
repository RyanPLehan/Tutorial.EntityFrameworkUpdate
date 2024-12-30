using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products;

public interface IProductRepository
{
    public Task<Product> Add(Product product, CancellationToken cancellationToken = default(CancellationToken));

    public Task Delete(Product product, CancellationToken cancellationToken = default(CancellationToken));
    public Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken));
    public Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken));
    public Task DeleteByCategory(int categoryId, CancellationToken cancellationToken = default(CancellationToken));

    public Task<Product?> Get(int id, CancellationToken cancellationToken = default(CancellationToken));
    public Task<ImmutableArray<Product>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken));

    public Task<ImmutableArray<Product>> FindByCategory(int categoryId, CancellationToken cancellationToken = default(CancellationToken));
    public Task<ImmutableArray<int>> FindIdByCategory(int categoryId, CancellationToken cancellationToken = default(CancellationToken));

    public Task ReplaceCategory(int oldCategoryId, int newCategoryId, CancellationToken cancellationToken = default(CancellationToken));

    public Task<Product?> Update(int id, string description, decimal price, int quantity, CancellationToken cancellationToken = default(CancellationToken));
    public Task UpdateDescription(int id, string description, CancellationToken cancellationToken = default(CancellationToken));
    public Task UpdatePrice(int id, decimal price, CancellationToken cancellationToken = default(CancellationToken));
    public Task UpdateQuantity(int id, int quantity, CancellationToken cancellationToken = default(CancellationToken));
}
