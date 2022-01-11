using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        [JsonProperty("issuer")]
        public string Issuer
        {
            get => _issuer; set => SetProperty(ref _issuer, value); 
        }

        [JsonProperty("name")]
        public string Name
        {
            get => _name; set => SetProperty(ref _name, value);
        }

        [JsonProperty("secret")]
        public string Secret
        {
            get => _secret; set => SetProperty(ref _secret, value);
        }

        [JsonProperty("period")]
        public int Period
        {
            get => _period; set => SetProperty(ref _period, value);
        }

        [JsonProperty("digits")]
        public int Digits
        {
            get => _digits; set => SetProperty(ref _digits, value); 
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountType Type
        {
            get => _type; set => SetProperty(ref _type, value);
        }

        [JsonProperty("algorithm")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountAlgorithm Algorithm
        {
            get => _algorithm; set => SetProperty(ref _algorithm, value); 
        }
    }
}
