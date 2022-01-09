using Seemon.Authenticator.Contracts.ViewModels;

namespace Seemon.Authenticator.Contracts.Views
{
    public interface IWindow : ICloseable
    {
        IViewModel ViewModel { get; }

        void Show();

        bool? ShowDialog(IWindow owner);
    }
}
