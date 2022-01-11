using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Helpers.Validators;
using Seemon.Authenticator.Helpers.ViewModels;
using System.Security;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private readonly IWindowManagerService _windowManagerService;

        private SecureString _currentPassword;
        private SecureString _newPassword;
        private SecureString _confirmPassword;

        private bool _showAlert = false;

        private ICommand _submitCommand;

        public ChangePasswordViewModel(IWindowManagerService windowManagerService) => _windowManagerService = windowManagerService;

        public ICommand SubmitCommand => _submitCommand ??= RegisterCommand(OnSubmit, CanSubmit);

        [SecureStringValidator(Description: "Current password")]
        public SecureString CurrentPassword
        {
            get => _currentPassword; set => SetProperty(ref _currentPassword, value, true);
        }

        [SecureStringValidator(Description:"New password")]
        public SecureString NewPassword
        {
            get => _newPassword; set => SetProperty(ref _newPassword, value, true);
        }

        [SecureStringValidator(Compare: nameof(NewPassword), Description: "Confirm password", CompareDescription: "New password")]
        public SecureString ConfirmPassword
        {
            get => _confirmPassword; set => SetProperty(ref _confirmPassword, value, true);
        }

        public bool ShowAlert
        {
            get => _showAlert; set => SetProperty(ref _showAlert, value);
        }

        public bool CanSubmit() => !HasErrors && CurrentPassword is not null && NewPassword is not null && ConfirmPassword is not null;

        public void OnSubmit() => _windowManagerService.GetWindow(PageKey)?.CloseDialog(true);
    }
}
