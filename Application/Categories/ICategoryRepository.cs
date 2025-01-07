using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories;

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
