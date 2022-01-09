using System;
using System.Windows;

using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Models;

using ControlzEx.Theming;

using MahApps.Metro.Theming;
using Seemon.Authenticator;
using Seemon.Authenticator.Models.Setttings;
using System.Windows.Media;

namespace Authenticator.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private readonly ISettingsService _settingsService;
        public ThemeSelectorService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void InitializeTheme() => SetTheme(GetCurrentTheme());

        public void SetTheme(ApplicationTheme theme)
        {
            if (theme.ToString() == ApplicationTheme.Default.ToString())
            {
                ThemeManager.Current.SyncTheme(ThemeSyncMode.SyncAll);
            }
            else
            {
                ThemeManager.Current.SyncTheme(ThemeSyncMode.SyncAll);
                if(theme.Base == ApplicationTheme.ThemeBase.System)
                {
                    var currentTheme = ThemeManager.Current.DetectTheme(Application.Current);
                    var inverseTheme = ThemeManager.Current.GetInverseTheme(currentTheme);

                    ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme(inverseTheme.BaseColorScheme, (Color)ColorConverter.ConvertFromString(theme.Accent)));
                    ThemeManager.Current.ChangeTheme(Application.Current, ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme(currentTheme.BaseColorScheme, (Color)ColorConverter.ConvertFromString(theme.Accent))));
                }
                else if( theme.Accent == "System")
                {
                    ThemeManager.Current.ChangeThemeBaseColor(Application.Current, theme.Base.ToString());
                }
                else
                {
                    var newTheme = RuntimeThemeGenerator.Current.GenerateRuntimeTheme(theme.Base.ToString(), (Color)ColorConverter.ConvertFromString(theme.Accent));
                    var inverseTheme = ThemeManager.Current.GetInverseTheme(newTheme);

                    ThemeManager.Current.AddTheme(inverseTheme);
                    ThemeManager.Current.ChangeTheme(Application.Current, ThemeManager.Current.AddTheme(newTheme));
                }
            }

            _settingsService.Set("application.theme", theme);
        }

        public ApplicationTheme GetCurrentTheme() => _settingsService.Get("application.theme", ApplicationTheme.Default);
    }
}
