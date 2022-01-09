using Microsoft.Toolkit.Mvvm.Input;
using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Helpers.ViewModels;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ICommand _goBackCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
        private ICommand _showSettingsCommand;
        private ICommand _showAboutCommand;

        public ShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand GoBackCommand => _goBackCommand ??= RegisterCommand(OnGoBack, CanGoBack);

        public ICommand LoadedCommand => _loadedCommand ??= RegisterCommand(OnLoaded);

        public ICommand UnloadedCommand => _unloadedCommand ??= RegisterCommand(OnUnloaded);

        public ICommand ShowSettingsCommand => _showSettingsCommand ??= RegisterCommand(OnShowSettings);

        public ICommand ShowAboutCommand => _showAboutCommand ??= RegisterCommand(OnShowAbout);

        private void OnLoaded() => _navigationService.Navigated += OnNavigated;

        private void OnUnloaded() => _navigationService.Navigated -= OnNavigated;

        private bool CanGoBack() => _navigationService.CanGoBack;

        private void OnGoBack() => _navigationService.GoBack();

        private void OnShowSettings() => _navigationService.NavigateTo(typeof(SettingsViewModel).FullName);

        private void OnShowAbout() => _navigationService.NavigateTo(typeof(AboutViewModel).FullName);

        private void OnNavigated(object sender, string viewModelName)
        {
            (GoBackCommand as RelayCommand).NotifyCanExecuteChanged();
        }
    }
}
