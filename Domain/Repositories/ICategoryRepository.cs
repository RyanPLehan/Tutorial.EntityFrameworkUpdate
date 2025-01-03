using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Repositories;

public interface ICategoryRepository
{
    public Task<Category> Add(Category category, CancellationToken cancellationToken = default);

    public Task Delete(Category category, CancellationToken cancellationToken = default);
    public Task Delete(int id, CancellationToken cancellationToken = default);


    public Task<Category?> Get(int id, CancellationToken cancellationToken = default);
    public Task<ImmutableArray<Category>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    public Task<ImmutableArray<Category>> GetAll(CancellationToken cancellationToken = default);

    public Task<Category> Update(Category category, CancellationToken cancellationToken = default);
}
