using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Seemon.Authenticator.Models.Settings
{
    public class WindowSettings : ObservableObject
    {
        private double _windowTop;
        private double _windowLeft;
        private double _windowHeight;
        private double _windowWidth;

        [JsonProperty("top")]
        public double WindowTop
        {
            get=> _windowTop; set => SetProperty(ref _windowTop, value); 
        }

        [JsonProperty("left")]
        public double WindowLeft
        {
            get => _windowLeft; set => SetProperty(ref _windowLeft, value);
        }

        [JsonProperty("height")]
        public double WindowHeight
        {
            get => _windowHeight; set => SetProperty(ref _windowHeight, value);
        }

        [JsonProperty("width")]
        public double WindowWidth
        {
            get => _windowWidth; set => SetProperty(ref _windowWidth, value);
        }
    }
}
