using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Helpers.Validators;
using Seemon.Authenticator.Helpers.ViewModels;
using System.Security;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class CreatePasswordViewModel : ViewModelBase
    {
        private readonly IWindowManagerService _windowManagerService;

        private SecureString _password;
        private SecureString _confirmPassword;

        private ICommand _submitCommand;

        public CreatePasswordViewModel(IWindowManagerService windowManagerService) => _windowManagerService = windowManagerService;

        public ICommand SubmitCommand => _submitCommand ??= RegisterCommand(OnSubmit, CanSubmit);


        [SecureStringValidator]
        public SecureString Password
        {
            get => _password; set => SetProperty(ref _password, value, true);
        }

        [SecureStringValidator(Compare: nameof(Password), Description:"Confirmation Password")]
        public SecureString ConfirmPassword
        {
            get => _confirmPassword; set => SetProperty(ref _confirmPassword, value, true);
        }

        private bool CanSubmit() => !HasErrors && Password != null && ConfirmPassword != null;

        private void OnSubmit() => _windowManagerService.GetWindow(PageKey)?.CloseDialog(true);
    }
}
