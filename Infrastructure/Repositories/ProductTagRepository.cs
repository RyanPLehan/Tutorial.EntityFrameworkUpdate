using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal sealed class ProductTagRepository : IProductTagRepository
{
    private readonly IContextFactory<InventoryContext> _contextFactory;

    public ProductTagRepository(IContextFactory<InventoryContext> roContextFactory)
    {
        ArgumentNullException.ThrowIfNull(roContextFactory, nameof(roContextFactory));

        _contextFactory = roContextFactory;
    }

    public async Task<ProductTag> Add(ProductTag productTag, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(productTag);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.ProductTags.Add(productTag);
            await context.SaveChangesAsync(cancellationToken);
        }

        return productTag;
    }

    public async Task<ImmutableArray<ProductTag>> Add(IEnumerable<ProductTag> productTags, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(productTags);
        if (!productTags.Any())
            return ImmutableArray<ProductTag>.Empty;

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.ProductTags.AddRange(productTags);
            await context.SaveChangesAsync(cancellationToken);
        }

        return productTags.ToImmutableArray();
    }


    public async Task Delete(ProductTag productTag, CancellationToken cancellationToken = default(CancellationToken))
    {
        ArgumentNullException.ThrowIfNull(productTag);

        using (var context = _contextFactory.CreateCommandContext())
        {
            context.ProductTags.Remove(productTag);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task Delete(int productId, int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.ProductTags
                         .Where(x => x.ProductId == productId &&
                                     x.Id == id)
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.ProductTags
                         .Where(x => ids.Contains(x.Id))
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task DeleteByProduct(int productId, CancellationToken cancellationToken = default)
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.ProductTags
                         .Where(x => x.ProductId == productId)
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task DeleteByProducts(IEnumerable<int> productIds, CancellationToken cancellationToken = default)
    {
        using (var context = _contextFactory.CreateCommandContext())
        {
            await context.ProductTags
                         .Where(x => productIds.Contains(x.ProductId))
                         .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task<ProductTag?> Get(int productId, int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        ProductTag? entity = null;
        using (var context = _contextFactory.CreateCommandContext())
        {
            entity = await context.ProductTags
                                  .Where(x => x.ProductId == productId &&
                                              x.Id == id)
                                  .FirstOrDefaultAsync(cancellationToken);
        }

        return entity;
    }


    public async Task<ImmutableArray<ProductTag>> FindByName(string name, CancellationToken cancellationToken = default(CancellationToken))
    {
        ProductTag[] entities;

        using (var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entities
            entities = await context.ProductTags
                                    .Where(x => x.Name == name)
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }

    public async Task<ImmutableArray<ProductTag>> FindByProduct(int productId, CancellationToken cancellationToken = default(CancellationToken))
    {
        ProductTag[] entities;

        using(var context = _contextFactory.CreateQueyContext())
        {
            // The query context, by default, does not enable tracking of the entities
            entities = await context.ProductTags
                                    .Where(x => x.ProductId == productId)
                                    .OrderBy(x => x.Name)
                                    .ToArrayAsync(cancellationToken);
        }

        return entities.ToImmutableArray();
    }
}
