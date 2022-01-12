using MahApps.Metro.Controls;
using Seemon.Authenticator.Contracts.ViewModels;
using Seemon.Authenticator.Contracts.Views;
using System.Windows;

namespace Seemon.Authenticator.Helpers.Views
{
    public class WindowBase : MetroWindow, IWindow
    {
        public IViewModel ViewModel => DataContext as IViewModel;

        public void CloseDialog(bool? response)
        {
            DialogResult = response;
            Close();
        }

        public bool? ShowDialog(IWindow owner)
        {
            Owner = (Window)owner;
            return ShowDialog();
        }
    }
}
