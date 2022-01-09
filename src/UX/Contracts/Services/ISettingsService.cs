namespace Seemon.Authenticator.Contracts.Services
{
    public interface ISettingsService
    {
        T Get<T>(string key, T @default);

        T Get<T>(string key);

        void Set<T>(string key, T value);
    }
}
