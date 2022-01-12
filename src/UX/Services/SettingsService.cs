using Seemon.Authenticator.Contracts.Services;
using System.Text.Json;
using System.Windows;

namespace Seemon.Authenticator.Core.Services
{
    public class SettingsService : ISettingsService
    {
        public T Get<T>(string key, T @default)
        {
            if (!Application.Current.Properties.Contains(key))
            {
                Application.Current.Properties.Add(key, @default);
                return @default;
            }
            return Get<T>(key);
        }

        public T Get<T>(string key)
        {
            if (!Application.Current.Properties.Contains(key))
            {
                return default;
            }

            if(Application.Current.Properties[key] is JsonElement)
            {
                var jObject = (JsonElement)Application.Current.Properties[key];
                Application.Current.Properties[key] = JsonSerializer.Deserialize<T>(jObject);
            }
            
            return (T)Application.Current.Properties[key];
        }

        public void Set<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;
        }
    }
}
