using System.Windows.Controls;

using Seemon.Authenticator.Contracts.Views;
using Seemon.Authenticator.ViewModels;

using MahApps.Metro.Controls;

namespace Seemon.Authenticator.Views
{
    public partial class ShellWindow : MetroWindow, IShellWindow
    {
        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}
