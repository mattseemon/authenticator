using System.Security;

namespace Seemon.Authenticator.Models.Security
{
    public class ChangePasswordResponse
    {
        public SecureString OldPassword { get; set; }

        public SecureString NewPassword { get; set; }
    }
}
