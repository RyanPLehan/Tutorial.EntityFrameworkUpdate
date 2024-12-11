using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;

public interface ICategoryRepository
{
    public Task<Category?> Get(int id, CancellationToken cancellationToken = default(CancellationToken));
    public Task<ImmutableArray<Category>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

    public Task<Category> Add(Category entity, CancellationToken cancellationToken = default(CancellationToken));
    public Task<Category> Update(Category entity, CancellationToken cancellationToken = default(CancellationToken));

    public Task Delete(Category entity, CancellationToken cancellationToken = default(CancellationToken));
    public Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken));
}
