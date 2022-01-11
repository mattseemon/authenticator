using Seemon.Authenticator.Helpers.Views;
using Seemon.Authenticator.ViewModels;

namespace Seemon.Authenticator.Views
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : WindowBase
    {
        public PasswordWindow(PasswordViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
