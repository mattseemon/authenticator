using System.Security;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IPasswordCacheService
    {
        SecureString Get();

        void Set(SecureString password);

        void Clear();
    }
}
