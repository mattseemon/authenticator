﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Seemon.Authenticator.Models.Setttings
{
    public class ApplicationTheme : ObservableObject
    {
        public enum ThemeBase
        {
            System,
            Light,
            Dark
        }

        private ThemeBase _base;
        private string _accent;

        [JsonConstructor()]
        public ApplicationTheme() { }

        [JsonProperty("base")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ThemeBase Base
        {
            get => _base; set => SetProperty(ref _base, value); 
        }

        [JsonProperty("accent")]
        public string Accent
        {
            get => _accent; set => SetProperty(ref _accent, value);
        }

        public override string ToString() => $"{Base}.{Accent}";

        public override bool Equals(object obj) => string.Equals(ToString(), obj.ToString());

        public override int GetHashCode() => ToString().GetHashCode();

        public static ApplicationTheme Default = Parse("System.System");

        public static ApplicationTheme Parse(string themeInfo)
        {
            var infos = themeInfo.Split(".");
            if(infos.Length != 2)
            {
                throw new ArgumentNullException(nameof(themeInfo), "Invalid theme info.");
            }
            Enum.TryParse(infos[0], out ThemeBase themeBase);

            return new ApplicationTheme
            {
                Base = themeBase,
                Accent = infos[1]
            };
        }
    }
}
