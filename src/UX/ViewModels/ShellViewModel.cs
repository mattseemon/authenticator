using Microsoft.Toolkit.Mvvm.Input;
using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.Views;
using Seemon.Authenticator.Helpers.Extensions;
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
        private ICommand _goToHomeCommand;

        public ShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand GoBackCommand => _goBackCommand ??= RegisterCommand(OnGoBack, CanGoBack);

        public ICommand LoadedCommand => _loadedCommand ??= RegisterCommand(OnLoaded);

        public ICommand UnloadedCommand => _unloadedCommand ??= RegisterCommand(OnUnloaded);

        public ICommand ShowSettingsCommand => _showSettingsCommand ??= RegisterCommand(OnShowSettings);

        public ICommand ShowAboutCommand => _showAboutCommand ??= RegisterCommand(OnShowAbout);

        public ICommand GoToHomeCommand => _goToHomeCommand ??= RegisterCommand(OnGoToHome, CanGoToHome);

        private void OnLoaded() => _navigationService.Navigated += OnNavigated;

        private void OnUnloaded() => _navigationService.Navigated -= OnNavigated;

        private bool CanGoBack() => _navigationService.CanGoBack;

        private void OnGoBack() => _navigationService.GoBack();

        private bool CanGoToHome()
        {
            var page = _navigationService.CurrentPage;
            if (page == null) return true;            
            return page.DataContext.GetType() != typeof(MainViewModel);
        }

        private void OnGoToHome() => _navigationService.NavigateTo(typeof(MainViewModel).FullName);

        private void OnShowSettings() => _navigationService.NavigateTo(typeof(SettingsViewModel).FullName);

        private void OnShowAbout() => _navigationService.NavigateTo(typeof(AboutViewModel).FullName);

        private void OnNavigated(object sender, string viewModelName)
        {
            RaiseCommandsCanExecute();
            /*(GoBackCommand as RelayCommand).NotifyCanExecuteChanged();
            (GoToHomeCommand as RelayCommand).NotifyCanExecuteChanged();*/
        }
    }
}
