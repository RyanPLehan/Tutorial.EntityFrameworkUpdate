using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly IContextFactory<ReadOnlyContext> _roContextFactory;
    //private readonly IContextFactory<ReadWriteContext> _rwContextFactory;

    public CategoryRepository(IContextFactory<ReadOnlyContext> roContextFactory)
    {
        ArgumentNullException.ThrowIfNull(roContextFactory, nameof(roContextFactory));

        _roContextFactory = roContextFactory;
    }

    public Task<Category> Add(Category entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    public Task Delete(Category entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> Get(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        Category? entity = null;

        using (var context = _roContextFactory.CreateQueyContext())
        {
            entity = await context.Categories
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync(cancellationToken);
        }

        return entity;
    }

    public async Task<ImmutableArray<Category>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
    {
        Category[] entities;

        using(var context = _roContextFactory.CreateQueyContext())
        {
            entities = await context.Categories
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }

    public Task<Category> Update(Category entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }
}
