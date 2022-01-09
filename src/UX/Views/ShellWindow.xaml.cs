using Seemon.Authenticator.Contracts.Views;
using Seemon.Authenticator.Helpers.Views;
using Seemon.Authenticator.ViewModels;
using System.Windows.Controls;

namespace Seemon.Authenticator.Views
{
    public partial class ShellWindow : WindowBase, IShellWindow
    {
        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public Frame GetNavigationFrame()
            => shellFrame;
    }
}
