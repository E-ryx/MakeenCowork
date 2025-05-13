using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Domain.Services
{
    public class MemoryService: IMemoryService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(5)
            };

            _memoryCache.Set(key, value, options);
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync(string key)
        {
            var found = _memoryCache.TryGetValue(key, out string? value);
            return Task.FromResult(found ? value : null);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
