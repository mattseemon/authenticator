using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Seemon.Authenticator.Models.Settings
{
    public class StorageSettings : ObservableObject
    {
        private string _location = string.Empty;
        private string _filename = ".storage";
        private bool _encrypted = false;
        private string _password = string.Empty;

        [JsonProperty("location")]
        public string Location
        {
            get => _location; set => SetProperty(ref _location, value);
        }

        [JsonProperty("filename")]
        public string Filename
        {
            get => _filename; set => SetProperty(ref _filename, value); 
        }

        [JsonProperty("encrypted")]
        public bool Encrypted
        {
            get => _encrypted; set => SetProperty(ref _encrypted, value);
        }

        [JsonProperty("password")]
        public string Password
        {
            get => _password; set => SetProperty(ref _password, value);
        }

        public static StorageSettings Default => new StorageSettings
        {
            Location = string.Empty,
            Filename = ".storage",
            Encrypted = false
        };
    }
}
