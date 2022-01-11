using System.Security;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IEncryptionService
    {
        string EncryptAndSign(string value, SecureString password);

        string VerifyAndDecrypt(string encryptedValue, SecureString password);
    }
}
