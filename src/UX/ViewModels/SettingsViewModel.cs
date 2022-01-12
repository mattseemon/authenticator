using MahApps.Metro.Controls.Dialogs;
using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.ViewModels;
using Seemon.Authenticator.Contracts.Views;
using Seemon.Authenticator.Helpers.ViewModels;
using Seemon.Authenticator.Helpers.Views;
using Seemon.Authenticator.Models.Settings;
using Seemon.Authenticator.Models.Setttings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Seemon.Authenticator.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INavigationAware
    {
        public class AccentColorData
        {
            public string Name { get; set; }
            public Brush ColorBrush { get; set; }
        }

        private readonly ISettingsService _settingsService;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IWindowManagerService _windowManagerService;
        private readonly ISystemService _systemService;
        private readonly IPasswordService _passwordService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IPasswordCacheService _passwordCacheService;
        private readonly IApplicationInfoService _applicationInfoService;
        private readonly ITaskbarIconService _taskbarIconService;

        private ApplicationTheme _theme;
        private SystemSettings _systemSettings;
        private StorageSettings _storageSettings;
        private SecuritySettings _securitySettings;

        private ICommand _selectionChangedCommand;
        private ICommand _checkedCommand;
        private ICommand _createPasswordCommand;
        private ICommand _removePasswordCommand;
        private ICommand _changePasswordCommand;
        private ICommand _browseCommand;

        public SettingsViewModel(ISettingsService settingsService, IThemeSelectorService themeSelectorService,
            IWindowManagerService windowManagerService, ISystemService systemService, IPasswordService passwordService,
            IPersistAndRestoreService persistAndRestoreService, IPasswordCacheService passwordCacheService,
            IApplicationInfoService applicationInfoService, ITaskbarIconService taskbarIconService)
        {
            _settingsService = settingsService;
            _themeSelectorService = themeSelectorService;
            _windowManagerService = windowManagerService;
            _systemService = systemService;
            _passwordService = passwordService;
            _persistAndRestoreService = persistAndRestoreService;
            _passwordCacheService = passwordCacheService;
            _applicationInfoService = applicationInfoService;
            _taskbarIconService = taskbarIconService;
        }

        public ICommand SelectionChangedCommand => _selectionChangedCommand ??= RegisterCommand<string>(OnSelectionChanged);

        public ICommand CheckedCommand => _checkedCommand ??= RegisterCommand<string>(OnChecked);

        public ICommand CreatePasswordCommand => _createPasswordCommand ??= RegisterCommand(OnCreatePassword, CanCreatePassword);

        public ICommand RemovePasswordCommand => _removePasswordCommand ??= RegisterCommand(OnRemovePassword, CanRemoveAndChangePassword);

        public ICommand ChangePasswordCommand => _changePasswordCommand ??= RegisterCommand(OnChangePassword, CanRemoveAndChangePassword);

        public ICommand BrowseCommand => _browseCommand ??= RegisterCommand(OnBrowse);

        public List<AccentColorData> AccentColors { get; set; }

        public ApplicationTheme Theme
        {
            get => _theme; set => SetProperty(ref _theme, value);
        }

        public SystemSettings System
        {
            get => _systemSettings; 
            set => SetProperty(ref _systemSettings, value);
        }

        public StorageSettings Storage
        {
            get => _storageSettings;
            set => SetProperty(ref _storageSettings, value);
        }

        public SecuritySettings Security
        {
            get => _securitySettings;
            set => SetProperty(ref _securitySettings, value);
        }

        public void OnNavigatedFrom() 
        {
            _persistAndRestoreService.PersistData();
        }

        public void OnNavigatedTo(object parameter)
        {
            AccentColors = new List<AccentColorData>();
            AccentColors.Add(new AccentColorData {  Name = "System", ColorBrush = SystemParameters.WindowGlassBrush });

            AccentColors.AddRange(typeof(Brushes)
                .GetProperties()
                .Where(prop => typeof(Brush).IsAssignableFrom(prop.PropertyType))
                .Select(prop => new AccentColorData { Name = prop.Name, ColorBrush = prop.GetValue(null) as Brush })
                .ToArray());

            AccentColors.RemoveAll(x => x.Name == "White");
            AccentColors.RemoveAll(x => x.Name == "Transparent");

            Theme = _settingsService.Get("application.theme", ApplicationTheme.Default);
            System = _settingsService.Get("settings.system", SystemSettings.Default);
            Storage = _settingsService.Get("settings.storage", StorageSettings.Default);
            Security = _settingsService.Get("settings.security", SecuritySettings.Default);
        }

        private void OnSelectionChanged(string parameter)
        {
            switch (parameter)
            {
                case "Theme":
                    _themeSelectorService.SetTheme(Theme);
                    break;
            }
            RaiseCommandsCanExecute();
        }

        private void OnChecked(string parameter)
        {
            switch (parameter)
            {
                case "TaskbarIcon":
                    if (System.ShowInNotification)
                    {
                        _taskbarIconService.Initialize();
                    }
                    else
                    {
                        _taskbarIconService.Destroy();
                    }
                    break;
                case "StartWithWindows":
                    try
                    {
                        _systemService.SetAutoStartWithWidndows(System.StartWithWindows);
                    }
                    catch (Exception) { }
                    break;
                case "AlwaysOnTop":
                    _windowManagerService.MainWindow.Topmost = System.AlwaysOnTop;
                    break;
            }
        }

        private bool CanCreatePassword() => !Storage.Encrypted;

        private void OnCreatePassword()
        {
            var password = _passwordService.CreatePassword();
            if (password != null)
            {
                // TODO: Encrypt the existing authenticator accounts

                Storage.Encrypted = true;
                Storage.Password = _passwordService.HashPassword(password);
                _passwordCacheService.Set(password);
            }
            RaiseCommandsCanExecute();
        }

        private bool CanRemoveAndChangePassword() => Storage.Encrypted;

        private async void OnRemovePassword()
        {
            IWindow window = _windowManagerService.GetWindow(PageKey);

            var options = new MetroDialogSettings { AffirmativeButtonText = "Yes", NegativeButtonText="No", ColorScheme= MetroDialogColorScheme.Inverted };
            var response = await ((WindowBase)window).ShowMessageAsync("Remove password protection!", "Are you sure you want to remove the password protection for Authenticator accounts?", MessageDialogStyle.AffirmativeAndNegative, options);

            if (response == MessageDialogResult.Affirmative)
            {
                var password = _passwordService.GetPassword(forcePrompt: true);
                if(password == null)
                {
                    return;
                }

                bool verified = false;
                while (!verified)
                {
                    verified = _passwordService.VerifyPassword(password, Storage.Password);
                    if (!verified)
                    {
                        password = _passwordService.GetPassword(true, true);
                    }
                    if(password == null)
                    {
                        return;
                    }   
                }
                if (verified)
                {
                    // TODO: Decrypt the existing authenticator accounts

                    Storage.Encrypted = false;
                    Storage.Password = String.Empty;
                    _passwordCacheService.Clear();
                }
            }
            RaiseCommandsCanExecute();
        }

        private void OnChangePassword()
        {
            var passwords = _passwordService.ChangePassword();
            if(passwords == null)
            {
                return;
            }

            bool verified = false;
            while (!verified)
            {
                verified = _passwordService.VerifyPassword(passwords.OldPassword, Storage.Password);
                if (!verified)
                {
                    passwords = _passwordService.ChangePassword(true);
                }
                if (passwords == null)
                {
                    return;
                }
            }
            if (verified)
            {
                // TODO: Decrypt and encrypt existing authenticator entires.

                Storage.Encrypted = true;
                Storage.Password = _passwordService.HashPassword(passwords.NewPassword);
                _passwordCacheService.Set(passwords.NewPassword);
            }
        }

        private async void OnBrowse()
        {
            var location = string.IsNullOrEmpty(Storage.Location) ? _applicationInfoService.DataPath : Storage.Location;
            location = _systemService.ShowFolderDialog("Select location to store authenticator accounts data file", location);
            if (!string.IsNullOrEmpty(location))
            {
                IWindow window = _windowManagerService.GetWindow(PageKey);
                var options = new MetroDialogSettings { AffirmativeButtonText = "Yes", NegativeButtonText = "No", ColorScheme = MetroDialogColorScheme.Inverted };
                var response = await ((WindowBase)window).ShowMessageAsync("Move accounts data file!", $"Are you sure you want to move the Authenticator accounts data file to '{location}'?", MessageDialogStyle.AffirmativeAndNegative, options);
                if(response == MessageDialogResult.Affirmative)
                {
                    // TODO: Move data file location
                    Storage.Location = location;
                }
            }
        }
    }
}
