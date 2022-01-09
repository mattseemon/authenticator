using Newtonsoft.Json.Linq;
using Seemon.Authenticator.Contracts.Services;
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

            if(Application.Current.Properties[key] is JObject)
            {
                var jObject = Application.Current.Properties[key] as JObject;
                Application.Current.Properties[key] = jObject.ToObject<T>();
            }
            if(Application.Current.Properties[key] is JArray)
            {
                var jArray = Application.Current.Properties[key] as JArray;
                Application.Current.Properties[key] = jArray.ToObject<T>();
            }

            return (T)Application.Current.Properties[key];
        }

        public void Set<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;
        }
    }
}
