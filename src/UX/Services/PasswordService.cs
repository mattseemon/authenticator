using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.Views;
using Seemon.Authenticator.Helpers.Services;
using Seemon.Authenticator.Models.Security;
using Seemon.Authenticator.ViewModels;
using Seemon.Authenticator.Views;
using System;
using System.Security;
using System.Windows;

namespace Seemon.Authenticator.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordCacheService _passwordCacheService;

        private const int _passwordSaltSize = 16;
        private const int _passwordHashSize = 32;

        public PasswordService(IPasswordCacheService passwordCacheService) => _passwordCacheService = passwordCacheService;

        public ChangePasswordResponse ChangePassword(bool retry = false)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                var window = App.GetService<ChangePasswordWindow>() as IWindow;
                var viewModel = window.ViewModel as ChangePasswordViewModel;
                viewModel.ShowAlert = retry;

                var response = window.ShowDialog(App.GetService<IShellWindow>());

                return response.HasValue && response.Value 
                    ? new ChangePasswordResponse { OldPassword = viewModel.CurrentPassword, NewPassword = viewModel.NewPassword } 
                    : null;
            });
        }

        public SecureString CreatePassword()
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                var window = App.GetService<CreatePasswordWindow>() as IWindow;
                var response = window.ShowDialog(App.GetService<IShellWindow>());
                var viewModel = window.ViewModel as CreatePasswordViewModel;

                return response.HasValue && response.Value ? viewModel.Password : null;
            });
        }

        public SecureString GetPassword(bool retry = false, bool forcePrompt = false)
        {
            SecureString password = null;
            if (!forcePrompt)
            {
                password = _passwordCacheService.Get();
            }
            return password ?? Application.Current.Dispatcher.Invoke(() =>
            {
                var window = App.GetService<PasswordWindow>() as IWindow;
                var viewModel = window.ViewModel as PasswordViewModel;
                viewModel.ShowAlert = retry;

                var response = window.ShowDialog(App.GetService<IShellWindow>());

                return response.HasValue && response.Value ? viewModel.Password : null;
            });
        }

        public string HashPassword(SecureString password)
        {
            var salt = EncryptionHelper.GenerateRandomBytes(_passwordSaltSize);
            var hash = EncryptionHelper.GenerateKey(password, salt, _passwordHashSize);

            byte[] merged = new byte[_passwordSaltSize + _passwordHashSize];
            salt.CopyTo(merged, 0);
            hash.CopyTo(merged, _passwordSaltSize);

            return Convert.ToBase64String(merged);
        }

        public bool VerifyPassword(SecureString password, string verificationHash)
        {
            var hash = Convert.FromBase64String(verificationHash);
            var salt = hash.AsSpan(0, _passwordSaltSize).ToArray();

            var computed = EncryptionHelper.GenerateKey(password, salt, _passwordHashSize);

            for(int i = 0; i < _passwordHashSize; i++)
            {
                if(hash[i+16] != computed[i])
                    return false;
            }
            return true;
        }
    }
}
