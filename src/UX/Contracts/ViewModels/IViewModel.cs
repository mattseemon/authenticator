namespace Seemon.Authenticator.Contracts.ViewModels
{
    public interface IViewModel
    {
        string PageKey { get; }

        void SetModel(object model);
    }
}
