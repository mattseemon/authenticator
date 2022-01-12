using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace Seemon.Authenticator.Models.Settings
{
    public class WindowSettings : ObservableObject
    {
        private double _windowTop;
        private double _windowLeft;
        private double _windowHeight;
        private double _windowWidth;

        [JsonPropertyName("top")]
        public double WindowTop
        {
            get=> _windowTop; set => SetProperty(ref _windowTop, value); 
        }

        [JsonPropertyName("left")]
        public double WindowLeft
        {
            get => _windowLeft; set => SetProperty(ref _windowLeft, value);
        }

        [JsonPropertyName("height")]
        public double WindowHeight
        {
            get => _windowHeight; set => SetProperty(ref _windowHeight, value);
        }

        [JsonPropertyName("width")]
        public double WindowWidth
        {
            get => _windowWidth; set => SetProperty(ref _windowWidth, value);
        }
    }
}
