using Seemon.Authenticator.Models.Setttings;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IThemeSelectorService
    {
        void InitializeTheme();

        void SetTheme(ApplicationTheme theme);

        ApplicationTheme GetCurrentTheme();
    }
}
