using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace Seemon.Authenticator.Models.Settings
{
    public class SystemSettings : ObservableObject
    {
        private bool _startWithWindows = false;
        private bool _alwaysOnTop = false;
        private bool _showInNotification = false;
        private bool _hideOnLaunch = false;
        private bool _minimizeToNotification = false;
        private bool _closeToNotification = false;
        

        [JsonPropertyName("startWithWindows")]
        public bool StartWithWindows
        {
            get => _startWithWindows; set => SetProperty(ref _startWithWindows, value); 
        }

        [JsonPropertyName("alwaysOnTop")]
        public bool AlwaysOnTop
        {
            get => _alwaysOnTop; 
            set => SetProperty(ref _alwaysOnTop, value);
        }

        [JsonPropertyName("showInNotification")]
        public bool ShowInNotification
        {
            get => _showInNotification; set => SetProperty(ref _showInNotification, value);
        }

        [JsonPropertyName("hideOnLaunch")]
        public bool HideOnLaunch
        {
            get => _hideOnLaunch; set => SetProperty(ref _hideOnLaunch, value);
        }

        [JsonPropertyName("minimizeToNotification")]
        public bool MinimizeToNotification
        {
            get => _minimizeToNotification; set => SetProperty(ref _minimizeToNotification, value);
        }

        [JsonPropertyName("closeToNotification")]
        public bool CloseToNotification
        {
            get => _closeToNotification; set => SetProperty(ref _closeToNotification, value);
        }

        public static SystemSettings Default => new SystemSettings()
        {
            StartWithWindows = false,
            AlwaysOnTop = false,
            ShowInNotification = false,
            MinimizeToNotification = false,
            CloseToNotification = false
        };
    }
}
