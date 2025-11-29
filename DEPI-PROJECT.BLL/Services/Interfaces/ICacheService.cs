using Microsoft.Extensions.Caching.Memory;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface ICacheService
    {
        public T? GetCached<T>(string key);
        public T CreateCached<T>(string key, T Data);
        public void InvalidateCache(string key);
    }
}