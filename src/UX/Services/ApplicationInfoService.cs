using Seemon.Authenticator.Contracts.Services;
using System;
using System.IO;
using System.Reflection;

namespace Seemon.Authenticator.Services
{
    public class ApplicationInfoService : IApplicationInfoService
    {
        private Assembly _assembly = Assembly.GetExecutingAssembly();
        private string _executablePath;
        private string _dataPath;

        private string _title;
        private string _author;
        private string _copyright;
        private string _version;
        private string _description;

        public ApplicationInfoService()
        {
            _executablePath = Path.GetDirectoryName(_assembly.Location);
            _title = GetAssemblyAttribute<AssemblyTitleAttribute>()?.Title ?? "Authenticator";
            _author = GetAssemblyAttribute<AssemblyCompanyAttribute>()?.Company ?? "Matt Seemon";
            _copyright = GetAssemblyAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? "© Copyright 2022, Matt Seemon. All rights reserved.";
            _description = GetAssemblyAttribute<AssemblyDescriptionAttribute>()?.Description ?? "Secure, Two-Factor Authenticator.";
            _version = GetAssemblyAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "NA";

            var path = Path.Combine(_executablePath, "data");
            _dataPath = Directory.Exists(path) ? path : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Identifier);

            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }
        }

        public string Title => _title;

        public string Version => _version;

        public string Author => _author;

        public string Description => _description;

        public string Copyright => _copyright;

        public string Identifier => $"Seemon.{Title}";

        public string ExecutablePath => _executablePath;

        public string DataPath => _dataPath;

        public string LogPath => Path.Combine(DataPath, "logs");

        private T GetAssemblyAttribute<T>() where T : Attribute
        {
            var attributes = _assembly.GetCustomAttributes(typeof(T), true);
            return attributes is null || attributes.Length == 0 ? null : (T)attributes[0];
        }
    }
}
