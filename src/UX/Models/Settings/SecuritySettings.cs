using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Seemon.Authenticator.Models.Settings
{
    public class SecuritySettings : ObservableObject
    {
        private bool _autoLock = true;
        private int _autoLockDuration = 300;

        public bool AutoLock
        {
            get => _autoLock; set => SetProperty(ref _autoLock, value); 
        }

        public int AutoLockDuration
        {
            get => _autoLockDuration; set => SetProperty(ref _autoLockDuration, value);
        }

        public static SecuritySettings Default = new SecuritySettings() { AutoLock = true, AutoLockDuration =  300 };
    }
}
