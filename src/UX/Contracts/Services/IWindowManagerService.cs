using Seemon.Authenticator.Contracts.Views;
using System.Windows;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IWindowManagerService
    {
        Window MainWindow { get; }

        IWindow GetWindow(string pageKey);

        void RestoreWindowSettings();

        void SaveWindowSettings();
    }
}
