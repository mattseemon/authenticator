using Microsoft.Win32;
using Seemon.Authenticator.Contracts.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Seemon.Authenticator.Services
{
    public class SystemService : ISystemService
    {
        private readonly IApplicationInfoService _applicationInfoService;
        private const string _autoStartKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public SystemService(IApplicationInfoService applicationInfoService)
        {
            _applicationInfoService = applicationInfoService;
        }

        public void OpenInWebBrowser(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var psi = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = url
                };
                Process.Start(psi);
            }
        }

        public void SetAutoStartWithWidndows(bool enable)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(_autoStartKey, true))
            {
                if(key == null)
                {
                    throw new Exception("Could not set application to auto start with windows.");
                }

                var identifier = _applicationInfoService.Identifier;
                var executable = Process.GetCurrentProcess().MainModule.FileName;

                if (enable)
                {
                    key.SetValue(identifier, $"\"{executable}\"");
                }
                else if (!enable && key.GetValue(identifier, null) != null)
                {
                    key.DeleteValue(identifier, false);
                }
            }
        }

        public string ShowFolderDialog(string description, string initialDirectory, bool showNewFolderButton = true)
        {
            if(!string.IsNullOrEmpty(initialDirectory) && !initialDirectory.EndsWith(Path.DirectorySeparatorChar))
            {
                initialDirectory += Path.DirectorySeparatorChar;
            }

            using var dialog = new FolderBrowserDialog
            {
                Description = description,
                UseDescriptionForTitle = true,
                AutoUpgradeEnabled = true,
                SelectedPath = initialDirectory,
                ShowNewFolderButton = showNewFolderButton,
                RootFolder = Environment.SpecialFolder.Desktop
            };

            return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : string.Empty;
        }
    }
}
