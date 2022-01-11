namespace Seemon.Authenticator.Contracts.Services
{
    public interface ISystemService
    {
        void OpenInWebBrowser(string url);

        void SetAutoStartWithWidndows(bool enable);

        string ShowFolderDialog(string description, string initialDirectory, bool showNewFolderButton = true);
    }
}
