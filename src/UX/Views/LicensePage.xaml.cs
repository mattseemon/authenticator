using Seemon.Authenticator.ViewModels;
using System.Windows.Controls;

namespace Seemon.Authenticator.Views
{
    /// <summary>
    /// Interaction logic for LicensePage.xaml
    /// </summary>
    public partial class LicensePage : Page
    {
        public LicensePage(LicenseViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
