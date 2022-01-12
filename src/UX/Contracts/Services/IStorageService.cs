using System;
using System.Collections.Generic;
using System.Security;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IStorageService
    {
        int Count { get; }

        bool InitializeStorage();

        void Save();

        void Destroy();

        void Clear();

        bool Exists(string key);

        T Get<T>(string key);

        void Set<T>(string key, T value);

        void Delete(string key);

        IReadOnlyCollection<string> GetKeys();

        IEnumerable<T> Query<T>(string key, Func<T, bool> predicate = null);

        void AddEncryption(SecureString password);

        void RemoveEncryption(SecureString password);

        void RotateEncryption(SecureString currentPassword, SecureString newPassword);
    }
}
