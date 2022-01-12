using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.Views;
using Seemon.Authenticator.Helpers.Extensions;
using Seemon.Authenticator.Models.Settings;
using System.Windows;

namespace Seemon.Authenticator.Services
{
    public class WindowManagerService : IWindowManagerService
    {
        private readonly ISettingsService _settingsService;

        public WindowManagerService(ISettingsService settingsService) => _settingsService = settingsService;

        public Window MainWindow => App.Current.MainWindow;

        public IWindow GetWindow(string pageKey)
        {
            foreach(Window window in Application.Current.Windows)
            {
                var dataContext = window.GetDataContext();
                if(dataContext?.PageKey == pageKey)
                {
                    return window as IWindow;
                }
            }
            return null;
        }

        public void RestoreWindowSettings()
        {
            var systemSettings = _settingsService.Get<SystemSettings>("settings.system");
            if(systemSettings != null)
            {
                MainWindow.Topmost = systemSettings.AlwaysOnTop;
                if(systemSettings.ShowInNotification && systemSettings.HideOnLaunch)
                {
                    if(systemSettings.MinimizeToNotification)
                        MainWindow.Hide();
                    else
                        MainWindow.WindowState = WindowState.Minimized;
                }
            }

            var windowSettings = _settingsService.Get<WindowSettings>("settings.windows");

            if(windowSettings != null)
            {
                MainWindow.Top = windowSettings.WindowTop;
                MainWindow.Left = windowSettings.WindowLeft;
                MainWindow.Height = windowSettings.WindowHeight;
                MainWindow.Width = windowSettings.WindowWidth;
            }

            ResizeToFit();
            MoveIntoView();
        }

        public void SaveWindowSettings()
        {
            var windowSettings = new WindowSettings
            {
                WindowTop = MainWindow.Top,
                WindowLeft = MainWindow.Left,
                WindowHeight = MainWindow.Height,
                WindowWidth = MainWindow.Width
            };

            _settingsService.Set("settings.windows", windowSettings);
        }

        private void ResizeToFit()
        {
            if(MainWindow.Height > SystemParameters.VirtualScreenHeight)
            {
                MainWindow.Height = SystemParameters.VirtualScreenHeight;
            }

            if(MainWindow.Width > SystemParameters.VirtualScreenWidth)
            {
                MainWindow.Width = SystemParameters.VirtualScreenWidth;
            }
        }

        private void MoveIntoView()
        {
            if (MainWindow.Top + MainWindow.Height / 2 > SystemParameters.VirtualScreenHeight)
            {
                MainWindow.Top = SystemParameters.VirtualScreenHeight - MainWindow.Height;
            }

            if (MainWindow.Left + MainWindow.Width / 2 > SystemParameters.VirtualScreenWidth)
            {
                MainWindow.Left = SystemParameters.VirtualScreenWidth - MainWindow.Width;
            }

            if (MainWindow.Top < 0) { MainWindow.Top = 0; }
            if (MainWindow.Left < 0) { MainWindow.Left = 0; }
        }
    }
}
