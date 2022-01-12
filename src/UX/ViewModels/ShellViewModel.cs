using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Helpers.ViewModels;
using Seemon.Authenticator.Models.Settings;
using System.ComponentModel;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;
        private readonly IWindowManagerService _windowManagerService;
        private readonly ITaskbarIconService _taskbarIconService;

        private ICommand _goBackCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
        private ICommand _closingCommand;
        private ICommand _showSettingsCommand;
        private ICommand _showAboutCommand;
        private ICommand _goToHomeCommand;

        public ShellViewModel(INavigationService navigationService, ISettingsService settingsService, 
            IWindowManagerService windowManagerService, ITaskbarIconService taskbarIconService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _windowManagerService = windowManagerService;
            _taskbarIconService = taskbarIconService;
        }

        public ICommand GoBackCommand => _goBackCommand ??= RegisterCommand(OnGoBack, CanGoBack);

        public ICommand LoadedCommand => _loadedCommand ??= RegisterCommand(OnLoaded);

        public ICommand UnloadedCommand => _unloadedCommand ??= RegisterCommand(OnUnloaded);

        public ICommand ClosingCommand => _closingCommand ??= RegisterCommand<object>(OnClosing);

        public ICommand ShowSettingsCommand => _showSettingsCommand ??= RegisterCommand(OnShowSettings);

        public ICommand ShowAboutCommand => _showAboutCommand ??= RegisterCommand(OnShowAbout);

        public ICommand GoToHomeCommand => _goToHomeCommand ??= RegisterCommand(OnGoToHome, CanGoToHome);

        private void OnLoaded()
        {
            _navigationService.Navigated += OnNavigated;
        }

        private void OnUnloaded() => _navigationService.Navigated -= OnNavigated;

        private void OnClosing(object parameter)
        {
            var e = (CancelEventArgs)parameter;
            var settings = _settingsService.Get<SystemSettings>("settings.system");
            _windowManagerService.SaveWindowSettings();

            if(settings.ShowInNotification && settings.CloseToNotification)
            {
                _taskbarIconService.Hide();
                e.Cancel = true;
            }
        }

        private bool CanGoBack() => _navigationService.CanGoBack;

        private void OnGoBack() => _navigationService.GoBack();

        private bool CanGoToHome()
        {
            var page = _navigationService.CurrentPage;
            return page == null || page.DataContext.GetType() != typeof(MainViewModel);
        }

        private void OnGoToHome() => _navigationService.NavigateTo(typeof(MainViewModel).FullName);

        private void OnShowSettings() => _navigationService.NavigateTo(typeof(SettingsViewModel).FullName);

        private void OnShowAbout() => _navigationService.NavigateTo(typeof(AboutViewModel).FullName);

        private void OnNavigated(object sender, string viewModelName)
        {
            RaiseCommandsCanExecute();
        }
    }
}
