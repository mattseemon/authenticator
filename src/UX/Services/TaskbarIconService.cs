using Hardcodet.Wpf.TaskbarNotification;
using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Models.Settings;
using Seemon.Authenticator.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Seemon.Authenticator.Services
{
    public class TaskbarIconService : ITaskbarIconService
    {
        private readonly IWindowManagerService _windowManagerService;
        private readonly ISettingsService _settingsService;

        private WindowState _storedWindowState = WindowState.Normal;
        private TaskbarIcon _taskbarIcon;

        public TaskbarIconService(IWindowManagerService windowManagerService, ISettingsService settingsService)
        {
            _windowManagerService = windowManagerService;
            _settingsService = settingsService;
        }

        public void Initialize()
        {
            var settings = _settingsService.Get<SystemSettings>("settings.system");

            if(settings != null && settings.ShowInNotification)
            {
                if(_windowManagerService.MainWindow is null)
                {
                    return;
                }

                if(_taskbarIcon is null)
                {
                    _taskbarIcon = new TaskbarIcon
                    {
                        DataContext = App.GetService<TaskbarIconViewModel>(),
                        IconSource = _windowManagerService.MainWindow.Icon,
                        ToolTipText = "Authenticator",
                        TrayToolTip = (UIElement)App.Current.FindResource("TBITooltip"),
                        ContextMenu = (ContextMenu)App.Current.FindResource("TBIContextMenu")
                    };
                    _taskbarIcon.TrayMouseDoubleClick += OnTrayMouseDoubleClick;
                    _windowManagerService.MainWindow.StateChanged += OnWindowStateChanged;
                }
            }
        }

        public void Show()
        {
            if(_windowManagerService.MainWindow is not null)
            {
                _windowManagerService.MainWindow.Show();
                _windowManagerService.MainWindow.WindowState = _storedWindowState;
                _windowManagerService.MainWindow.Activate();
            }
        }

        public void Hide()
        {
            if(_windowManagerService.MainWindow is not null)
            {
                _windowManagerService.MainWindow.Hide();
            }
        }

        public void Destroy()
        {
            if(_taskbarIcon is not null)
            {
                if(_windowManagerService.MainWindow is not null)
                {
                    _windowManagerService.MainWindow.StateChanged -= OnWindowStateChanged;
                }
                _taskbarIcon.Dispose();
                _taskbarIcon = null;
            }
        }

        private void OnWindowStateChanged(object sender, System.EventArgs e)
        {
            var settings = _settingsService.Get<SystemSettings>("settings.system");
            if(_windowManagerService.MainWindow.WindowState == WindowState.Minimized)
            {
                if(settings.ShowInNotification && settings.MinimizeToNotification)
                {
                    Hide();
                }
            }
            else
            {
                _storedWindowState = _windowManagerService.MainWindow.WindowState;
            }
        }

        private void OnTrayMouseDoubleClick(object sender, RoutedEventArgs e) => Show();

        
    }
}
