using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Helpers.Validators;
using Seemon.Authenticator.Helpers.ViewModels;
using System.Security;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class PasswordViewModel : ViewModelBase
    {
        private readonly IWindowManagerService _windowManagerService;

        private SecureString _password;
        private bool _showAlert;

        private ICommand _submitCommand;

        public PasswordViewModel(IWindowManagerService windowManagerService) => _windowManagerService = windowManagerService;

        [SecureStringValidator]
        public SecureString Password
        {
            get => _password; set => SetProperty(ref _password, value, true);
        }

        public bool ShowAlert
        {
            get => _showAlert; set => SetProperty(ref _showAlert, value);
        }

        public ICommand SubmitCommand => _submitCommand ??= RegisterCommand(OnSubmit, CanSubmit);

        private bool CanSubmit() => !HasErrors && Password != null;

        private void OnSubmit() => _windowManagerService.GetWindow(PageKey)?.CloseDialog(true);
    }
}
