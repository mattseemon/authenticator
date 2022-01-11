using Seemon.Authenticator.Helpers.Views;
using Seemon.Authenticator.ViewModels;

namespace Seemon.Authenticator.Views
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : WindowBase
    {
        public ChangePasswordWindow(ChangePasswordViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
