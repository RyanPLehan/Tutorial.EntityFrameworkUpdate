using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Decorators;

internal sealed class CachedCategoryRepository : ICategoryRepository
{
    private const string CACHE_KEY_PREFIX = "CategoryRepository";
    private const string CACHE_KEY_SUFFIX_DEFAULT = "All";
    private const int DEFAULT_EXPIRATION_TIME_IN_SECONDS = 60 * 15;      // 15 minutes
    private static MemoryCacheEntryOptions DefaultCacheEntryOptions = null;

    private readonly IMemoryCache _cache;
    private readonly ICategoryRepository _repository;

    public CachedCategoryRepository(IMemoryCache cache,
                                    ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(cache, nameof(cache));
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _cache = cache;
        _repository = repository;

        if (DefaultCacheEntryOptions == null)
            DefaultCacheEntryOptions = CreateMemoryCacheEntryOptions(DEFAULT_EXPIRATION_TIME_IN_SECONDS);
    }


    public async Task<Category> Add(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        Category repoEntity = await _repository.Add(category, cancellationToken);
        RemoveCacheEntry(CreateCacheKey());

        return repoEntity;
    }

    public async Task Delete(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.Delete(category, cancellationToken);
        RemoveCacheEntry(CreateCacheKey());
    }

    public async Task Delete(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.Delete(id, cancellationToken);
        RemoveCacheEntry(CreateCacheKey());
    }

    public async Task Delete(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken))
    {
        await _repository.Delete(ids, cancellationToken);
        RemoveCacheEntry(CreateCacheKey());
    }

    public async Task<Category?> Get(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        ImmutableArray<Category> entities = await this.GetAll(cancellationToken);    // Read from cache
        return entities.Where(x => x.Id == id)
                       .FirstOrDefault();
    }

    public async Task<ImmutableArray<Category>> Get(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken))
    {
        ImmutableArray<Category> entities = await this.GetAll(cancellationToken);    // Read from cache
        return entities.Where(x => ids.Contains(x.Id))
                       .ToImmutableArray();
    }

    public async Task<ImmutableArray<Category>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
    {
        string cacheKey = CreateCacheKey();
        ImmutableArray<Category> values;

        if (!_cache.TryGetValue<ImmutableArray<Category>>(cacheKey, out values))
        {
            values = await _repository.GetAll(cancellationToken);
            _cache.Set<ImmutableArray<Category>>(cacheKey, values, DefaultCacheEntryOptions);
        }

        return values;
    }

    public async Task<Category> Update(Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        Category repoEntity = await _repository.Update(category, cancellationToken);
        RemoveCacheEntry(CreateCacheKey());

        return repoEntity;
    }



    private MemoryCacheEntryOptions CreateMemoryCacheEntryOptions(int entryExpirationInSeconds)
    {
        return new MemoryCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromSeconds(entryExpirationInSeconds),
        };
    }

    private void RemoveCacheEntry(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }

    private string CreateCacheKey(string keySuffix = CACHE_KEY_SUFFIX_DEFAULT)
    {
        return string.Format("{0}_{1}", CACHE_KEY_PREFIX, keySuffix);
    }


}
