using Common.DependencyInjection.Interfaces;
using Common.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure;

public class MemoryCachedService : ICachedService, IScoped
{
    private readonly IMemoryCache _cache;
    private MemoryCacheEntryOptions _options;

    public MemoryCachedService(IMemoryCache cache)
    {
        _cache = cache;
        _options = new();
    }

    public void Dispose() => _cache.Dispose();

    public void SetExpiration(TimeSpan timeSpan)
    {
        _options.SetAbsoluteExpiration(timeSpan);
    }

    public ICacheEntry CreateEntry(object key) => _cache.CreateEntry(key);

    public void Remove(object key) => _cache.Remove(key);


    public bool TryGetValue(object key, out object value) => _cache.TryGetValue(key, out value);

    public Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory) {
        
       return _cache.GetOrCreateAsync(key, (e) =>
       {
           e.SetOptions(_options);
           return factory(e);
       });
    }

    public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory) => _cache.GetOrCreate(key, factory);
}