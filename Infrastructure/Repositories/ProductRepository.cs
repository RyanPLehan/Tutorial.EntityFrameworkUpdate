using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly IContextFactory<InventoryContext> _contextFactory;

    public ProductRepository(IContextFactory<InventoryContext> roContextFactory)
    {
        ArgumentNullException.ThrowIfNull(roContextFactory, nameof(roContextFactory));

        _contextFactory = roContextFactory;
    }

    public async Task<Product> Add(Product product, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(product);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.Products.Add(product);
            await context.SaveChangesAsync(cancellationToken);
        }

        return product;
    }

    public async Task Delete(Product product, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(product);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Products
                         .Where(x => x.Id == id)
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Products
                         .Where(x => ids.Contains(x.Id))
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task DeleteByCategory(int id, CancellationToken cancellationToken = default)
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Products
                         .Where(x => x.CategoryId == id)
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task<Product?> Get(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        Product? entity = null;

        using (var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entity
            entity = await context.Products
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync(cancellationToken);
        }

        return entity;
    }

    public async Task<ImmutableArray<Product>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken))
    {
        Product[] entities;

        using (var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entities
            entities = await context.Products
                                    .Where(x => ids.Contains(x.Id))
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }

    public async Task<ImmutableArray<Product>> GetByCategory(int categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
        Product[] entities;

        using(var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entities
            entities = await context.Products
                                    .Where(x => x.CategoryId == categoryId)
                                    .OrderBy(x => x.Name)
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }

    public async Task ReplaceCategory(int oldCategoryId, int newCategoryId, CancellationToken cancellationToken = default)
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Products
                         .Where(x => x.CategoryId == oldCategoryId)
                         .ExecuteUpdateAsync(x => 
                            x.SetProperty(p => p.CategoryId, newCategoryId), 
                            cancellationToken);
        }
    }

    public async Task<Product?> Update(int id, string description, decimal price, int quantity, CancellationToken cancellationToken = default)
    {
        // Since we only want EF to generate a PARTIAL UPDATE statement, we need to do the following
        // 1. Get the entity that we are going to update
        // 2. Make sure the entity is being Tracked
        // 3. Overwrite the fields/properties of the Tracked Entity
        // 4. Issue an update
        // 5. Save the changes

        Product? entity = null;
        using (var context = _contextFactory.CreateCommandContext())
        {
            // Since we are using the Command context and not explicitly using AsNoTracking, the entity will be tracked
            entity = await context.Products
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Description = description;
                entity.Price = price;
                entity.Quantity = quantity;

                context.Products.Update(entity);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        return entity;
    }

    public async Task UpdateDescription(int id, string description, CancellationToken cancellationToken = default)
    {
        // Since we only want EF to generate a PARTIAL UPDATE statement only for the 'Description' field
        // 1. Issue the ExecuteUpdate
        // Notes:
        //  1. No need to get the entity first
        //  2. Must explicitly set the where clause
        //  3. A good use case would be for HTTP PATCH methods
        //  4. Not returning an entity

        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Products
                         .Where(x => x.Id == id)
                         .ExecuteUpdateAsync(x =>
                            x.SetProperty(p => p.Description, description),
                            cancellationToken);
        }
    }

    public async Task UpdatePrice(int id, decimal price, CancellationToken cancellationToken = default)
    {
        // Since we only want EF to generate a PARTIAL UPDATE statement only for the 'Price' field
        // 1. Issue the ExecuteUpdate
        // Notes:
        //  1. No need to get the entity first
        //  2. Must explicitly set the where clause
        //  3. A good use case would be for HTTP PATCH methods
        //  4. Not returning an entity

        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Products
                         .Where(x => x.Id == id)
                         .ExecuteUpdateAsync(x =>
                            x.SetProperty(p => p.Price, price),
                            cancellationToken);
        }
    }

    public async Task UpdateQuantity(int id, int quantity, CancellationToken cancellationToken = default)
    {
        // Since we only want EF to generate a PARTIAL UPDATE statement only for the 'Quantity' field
        // 1. Issue the ExecuteUpdate
        // Notes:
        //  1. No need to get the entity first
        //  2. Must explicitly set the where clause
        //  3. A good use case would be for HTTP PATCH methods
        //  4. Not returning an entity

        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.Products
                         .Where(x => x.Id == id)
                         .ExecuteUpdateAsync(x =>
                            x.SetProperty(p => p.Quantity, quantity),
                            cancellationToken);
        }
    }

}
