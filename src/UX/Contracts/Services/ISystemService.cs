namespace Seemon.Authenticator.Contracts.Services
{
    public interface ISystemService
    {
        void OpenInWebBrowser(string url);

        void SetAutoStartWithWidndows(bool enable);
    }
}
