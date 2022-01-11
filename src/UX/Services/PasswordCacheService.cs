using Microsoft.Extensions.Caching.Memory;
using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Models.Settings;
using System;
using System.Security;

namespace Seemon.Authenticator.Services
{
    public class PasswordCacheService : IPasswordCacheService
    {
        private readonly ISettingsService _settingsService;

        private readonly MemoryCache _cache;
        private readonly string _passwordCacheKey = Guid.NewGuid().ToString();

        public PasswordCacheService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 100 });
        }

        public void Clear() => _cache.Remove(_passwordCacheKey);

        public SecureString Get() => _cache.TryGetValue(_passwordCacheKey, out SecureString password) ? password : null;

        public void Set(SecureString password)
        {
            var securitySettings = _settingsService.Get("settings.security", SecuritySettings.Default);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(1);

            if (securitySettings.AutoLock)
            {
                cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromSeconds(securitySettings.AutoLockDuration));
            }

            _cache.Set(_passwordCacheKey, password, cacheEntryOptions);
        }
    }
}
