using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.ViewModels;
using Seemon.Authenticator.Helpers.ViewModels;
using Seemon.Authenticator.Models.Settings;
using Seemon.Authenticator.Models.Setttings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private ApplicationTheme _theme;
        private SystemSettings _systemSettings;

        private ICommand _selectionChangedCommand;
        private ICommand _checkedCommand;

        public SettingsViewModel(ISettingsService settingsService, IThemeSelectorService themeSelectorService,
            IWindowManagerService windowManagerService, ISystemService systemService)
        {
            _settingsService = settingsService;
            _themeSelectorService = themeSelectorService;
            _windowManagerService = windowManagerService;
            _systemService = systemService;
        }

        public ICommand SelectionChangedCommand => _selectionChangedCommand ??= RegisterCommand<string>(OnSelectionChanged);

        public ICommand CheckedCommand => _checkedCommand ??= RegisterCommand<string>(OnChecked);

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

        public void OnNavigatedFrom() { }

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
                        //TODO: notification icon initialization
                    }
                    else
                    {
                        //TODO: notification icon destruction
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
    }
}
