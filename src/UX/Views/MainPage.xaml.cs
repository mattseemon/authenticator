using System.Windows.Controls;

using Seemon.Authenticator.ViewModels;

namespace Seemon.Authenticator.Views
{
    public partial class MainPage : Page
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
