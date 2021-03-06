using Seemon.Authenticator.Contracts.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Seemon.Authenticator.Helpers.Extensions
{
    public static class FrameExtensions
    {
        public static IViewModel GetDataContext(this Frame frame) 
            => frame.Content is FrameworkElement element ? element.DataContext as IViewModel : null;

        public static void CleanNavigation(this Frame frame)
        {
            while (frame.CanGoBack)
            {
                frame.RemoveBackEntry();
            }
        }
    }
}
