using System.Windows.Controls;

namespace Seemon.Authenticator.Contracts.Views
{
    public interface IShellWindow : IWindow
    {
        Frame GetNavigationFrame();
    }
}
