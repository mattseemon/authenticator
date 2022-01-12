using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace Seemon.Authenticator.Models
{
    public enum AccountType
    {
        TOTP,
        HOTP
    }
    public enum AccountAlgorithm
    {
        SHA1,
        SHA256,
        SHA512
    }

    public class Account : ObservableObject
    {
        private string _issuer;
        private string _name;
        private string _secret;
        private int _period = 30;
        private int _digits = 6;
        private AccountType _type = AccountType.TOTP;
        private AccountAlgorithm _algorithm = AccountAlgorithm.SHA1;

        [JsonPropertyName("issuer")]
        public string Issuer
        {
            get => _issuer; set => SetProperty(ref _issuer, value); 
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get => _name; set => SetProperty(ref _name, value);
        }

        [JsonPropertyName("secret")]
        public string Secret
        {
            get => _secret; set => SetProperty(ref _secret, value);
        }

        [JsonPropertyName("period")]
        public int Period
        {
            get => _period; set => SetProperty(ref _period, value);
        }

        [JsonPropertyName("digits")]
        public int Digits
        {
            get => _digits; set => SetProperty(ref _digits, value); 
        }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountType Type
        {
            get => _type; set => SetProperty(ref _type, value);
        }

        [JsonPropertyName("algorithm")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountAlgorithm Algorithm
        {
            get => _algorithm; set => SetProperty(ref _algorithm, value); 
        }
    }
}
