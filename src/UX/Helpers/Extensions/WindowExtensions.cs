using MahApps.Metro.Controls.Dialogs;
using Seemon.Authenticator.Contracts.ViewModels;
using Seemon.Authenticator.Contracts.Views;
using Seemon.Authenticator.Helpers.Views;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Seemon.Authenticator.Helpers.Extensions
{
    public static class WindowExtensions
    {
        private static readonly MetroDialogSettings dialogSettings = new MetroDialogSettings
        {
            ColorScheme = MetroDialogColorScheme.Accented,
            AffirmativeButtonText = "OK",
            NegativeButtonText = "Cancel"
        };

        public static IViewModel GetDataContext(this Window window) => (IViewModel)(window.FindName("ShellFrame") is Frame frame
            ? frame.GetDataContext()
            : window.DataContext ?? null);

        public static async Task<ProgressDialogController> ShowProgressPromptAsyc(this IWindow window, string title, string message)
            => await ((WindowBase)window).ShowProgressAsync(title, message, settings: dialogSettings);

        public static async Task<MessageDialogResult> ShowMessagePromptAsyc(this IWindow window, string title, string message, MessageDialogStyle style, MetroDialogSettings settings = null)
            => await((WindowBase)window).ShowMessageAsync(title, message, style, settings ?? dialogSettings);
    }
}
