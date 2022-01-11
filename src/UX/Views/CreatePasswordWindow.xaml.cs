using Seemon.Authenticator.Helpers.Views;
using Seemon.Authenticator.ViewModels;

namespace Seemon.Authenticator.Views
{
    /// <summary>
    /// Interaction logic for CreatePasswordWindow.xaml
    /// </summary>
    public partial class CreatePasswordWindow : WindowBase
    {
        public CreatePasswordWindow(CreatePasswordViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
