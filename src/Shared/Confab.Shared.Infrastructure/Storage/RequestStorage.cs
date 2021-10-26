using System;
using Confab.Shared.Abstractions.Storage;
using Microsoft.Extensions.Caching.Memory;

namespace Confab.Shared.Infrastructure.Storage
{
    internal sealed class RequestStorage : IRequestStorage
    {
        private readonly IMemoryCache _cache;

        public RequestStorage(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set<T>(string key, T value, TimeSpan? duration = null)
        {
            _cache.Set(key, value, duration ?? TimeSpan.FromSeconds(5));
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }
    }
}