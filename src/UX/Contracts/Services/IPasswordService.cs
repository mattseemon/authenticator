using Seemon.Authenticator.Models.Security;
using System.Security;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IPasswordService
    {
        SecureString CreatePassword();

        SecureString GetPassword(bool retry = false, bool forcePrompt = false);

        ChangePasswordResponse ChangePassword(bool retry = false);

        string HashPassword(SecureString password);

        bool VerifyPassword(SecureString password, string verificationHash);
    }
}
