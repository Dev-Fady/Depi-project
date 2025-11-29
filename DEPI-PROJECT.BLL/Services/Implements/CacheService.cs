using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CacheService(IMemoryCache memoryCache,
                            IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
        }
        public T? GetCached<T>(string key)
        {
            if(_memoryCache.TryGetValue(key, out T? cacheValue))
            {
                return cacheValue;
            }
            return cacheValue;
        }

        public T CreateCached<T>(string key, T Data)
        {
            if(_memoryCache.TryGetValue(key, out T? CachedValue) == true)
            {
                return CachedValue!;
            }
            
            if(!int.TryParse(_configuration.GetSection("CachingSettings").GetSection("ValidMintues").Value, out int cacheMinutes)){
                throw new Exception("No value exists in section \"ValidMintues\" in \"CachingSettings\"");
            }


            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(cacheMinutes));

            _memoryCache.Set(key, Data, cacheEntryOptions);
            return Data;
        }

        public void InvalidateCache(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}