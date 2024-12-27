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
            await context.SaveChangesAsync(cancellationToken);
        }

        return category;
    }

    public async Task Delete(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(category);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Categories
                         .Where(x => x.Id == id)
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Categories
                         .Where(x => ids.Contains(x.Id))
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task<Category?> Get(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        Category? entity = null;

        using (var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entity
            entity = await context.Categories
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync(cancellationToken);
        }

        return entity;
    }

    public async Task<ImmutableArray<Category>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken))
    {
        Category[] entities;

        using (var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entities
            entities = await context.Categories
                                    .Where(x => ids.Contains(x.Id))
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }

    public async Task<ImmutableArray<Category>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
    {
        Category[] entities;

        using(var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entities
            entities = await context.Categories
                                    .OrderBy(x => x.Name)
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }

    public async Task<Category> Update(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        // Since we only want EF to generate a FULL UPDATE statement, we need to do the following
        // 1. Add the object being passed in to the context using the Update method
        //    The object being passed in is NOT being tracked
        // 2. Save the changes

        ArgumentNullException.ThrowIfNull(category);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync(cancellationToken);
        }

        return category;
    }
}
