using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Models.Settings;
using Seemon.Authenticator.Models.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Seemon.Authenticator.Services
{
    public class StorageService : IStorageService
    {
        private readonly IApplicationInfoService _applicationInfoService;
        private readonly ISettingsService _settingsService;
        private readonly IEncryptionService _encryptionService;
        private readonly IFileService _fileService;
        private readonly IPasswordService _passwordService;
        private readonly IPasswordCacheService _passwordCacheService;

        public StorageService(IApplicationInfoService applicationInfoService, ISettingsService settingsService,
            IEncryptionService encryptionService, IFileService fileService, IPasswordCacheService passwordCacheService,
            IPasswordService passwordService)
        {
            _applicationInfoService = applicationInfoService;
            _settingsService = settingsService;
            _encryptionService = encryptionService;
            _fileService = fileService;
            _passwordCacheService = passwordCacheService;
            _passwordService = passwordService;
        }

        private Store Storage { get; set; } = new Store();

        public int Count => Storage.Count;

        public bool InitializeStorage()
        {
            var settings = _settingsService.Get<StorageSettings>("settings.storage");

            if(string.IsNullOrEmpty(settings.Location))
            {
                settings.Location = _applicationInfoService.DataPath;
                settings.Filename = "accounts.storage";
                _settingsService.Set("settings.storage", settings);
            }

            var storagePath = Path.Combine(settings.Location, settings.Filename);
            if (!File.Exists(storagePath))
            {
                _fileService.Save(settings.Location, settings.Filename, Storage);
            }
            Storage = _fileService.Read<Store>(settings.Location, settings.Filename);

            if(settings.Encrypted && !string.IsNullOrEmpty(settings.Password))
            {
                var password = _passwordService.GetPassword();
                if (password is null)
                {
                    return false;
                }

                bool verified = false;
                while (!verified)
                {
                    verified = _passwordService.VerifyPassword(password, settings.Password);
                    if (!verified)
                    {
                        password = _passwordService.GetPassword(true);
                    }
                    if(password is null)
                    {
                        return false;
                    }
                }
                if (verified)
                {
                    _passwordCacheService.Set(password);
                }
            }
            return true;
        }

        public void Clear()
        {
            Storage.Clear();
        }

        public void Delete(string key)
        {
            Storage.Remove(key);
        }

        public void Destroy()
        {
            var settings = _settingsService.Get<StorageSettings>("settings.storage");
            if(File.Exists(Path.Combine(settings.Location, settings.Filename)))
            {
                _fileService.Delete(settings.Location, settings.Filename);
            }
        }

        public bool Exists(string key)
        {
            return Storage.ContainsKey(key);
        }

        public T Get<T>(string key)
        {
            var settings = _settingsService.Get<StorageSettings>("settings.storage");

            var success = Storage.TryGetValue(key, out string raw);
            if (!success) throw new ArgumentNullException($"Could not find item with '{key}' in the data store.");

            if (settings.Encrypted && !string.IsNullOrEmpty(settings.Password))
            {
                raw = _encryptionService.VerifyAndDecrypt(raw, _passwordService.GetPassword());
            }

            return JsonSerializer.Deserialize<T>(raw);
        }

        public IReadOnlyCollection<string> GetKeys()
        {
            return Storage.Keys.OrderBy(x => x).ToList();
        }

        public IEnumerable<T> Query<T>(string key, Func<T, bool> predicate = null)
        {
            var collection = Get<IEnumerable<T>>(key);
            return predicate == null ? collection : collection.Where(predicate);
        }

        public void Save()
        {
            var settings = _settingsService.Get<StorageSettings>("settings.storage");
            _fileService.Save(settings.Location, settings.Filename, Storage);
        }

        public void Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if(value is null) throw new ArgumentNullException(nameof(value));

            var raw = JsonSerializer.Serialize(value);
            if (Storage.Keys.Contains(key))
            {
                Storage.Remove(key);
            }

            var settings = _settingsService.Get<StorageSettings>("settings.storage");
            if (settings.Encrypted && !string.IsNullOrEmpty(settings.Password))
            {
                raw = _encryptionService.EncryptAndSign(raw, _passwordService.GetPassword());
            }

            Storage.Add(key, raw);

            Save();
        }

        public void AddEncryption(System.Security.SecureString password)
        {
            var tempStore = new Store();
            foreach(var key in Storage.Keys)
            {
                var raw = Storage[key];
                raw = _encryptionService.EncryptAndSign(raw, password);
                tempStore.Add(key, raw);
            }

            Storage.Clear();
            Storage = tempStore;
            Save();
        }

        public void RemoveEncryption(System.Security.SecureString password)
        {
            var settings = _settingsService.Get<StorageSettings>("settings.storage");

            if (settings.Encrypted && !string.IsNullOrEmpty(settings.Password))
            {
                var tempStore = new Store();
                foreach (var key in Storage.Keys)
                {
                    var raw = Storage[key];
                    raw = _encryptionService.VerifyAndDecrypt(raw, password);
                    tempStore.Add(key, raw);
                }

                Storage.Clear();
                Storage = tempStore;
            }
            Save();
        }

        public void RotateEncryption(System.Security.SecureString currentPassword, System.Security.SecureString newPassword)
        {
            var settings = _settingsService.Get<StorageSettings>("settings.storage");

            if (settings.Encrypted && !string.IsNullOrEmpty(settings.Password))
            {
                var tempStore = new Store();
                foreach (var key in Storage.Keys)
                {
                    var raw = Storage[key];
                    raw = _encryptionService.VerifyAndDecrypt(raw, currentPassword);
                    raw = _encryptionService.EncryptAndSign(raw, newPassword);
                    tempStore.Add(key, raw);
                }

                Storage.Clear();
                Storage = tempStore;
            }
            Save();
        }
    }
}
