using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;

public interface ICategoryRepository
{
    public Task<Category> Add(Category category, CancellationToken cancellationToken = default(CancellationToken));

    public Task Delete(Category category, CancellationToken cancellationToken = default(CancellationToken));
    public Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken));
    public Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken));

    public Task<Category?> Get(int id, CancellationToken cancellationToken = default(CancellationToken));
    public Task<ImmutableArray<Category>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken));
    public Task<ImmutableArray<Category>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

    public Task<Category> Update(Category category, CancellationToken cancellationToken = default(CancellationToken));
}
