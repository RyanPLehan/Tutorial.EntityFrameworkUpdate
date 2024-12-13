using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly IContextFactory<InventoryContext> _contextFactory;

    public CategoryRepository(IContextFactory<InventoryContext> roContextFactory)
    {
        ArgumentNullException.ThrowIfNull(roContextFactory, nameof(roContextFactory));

        _contextFactory = roContextFactory;
    }

    public async Task<Category> Add(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(category);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        return category;
    }

    public async Task Delete(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(category);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Category?> Get(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        Category? entity = null;

        using (var context = _contextFactory.CreateQueyContext())
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

        using(var context = _contextFactory.CreateQueyContext())
        {
            entities = await context.Categories
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }

    public Task<Category> Update(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(category);

        throw new NotImplementedException();
    }
}
